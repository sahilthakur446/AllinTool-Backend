using AllinTool.Data.Models;
using DotNetTools.SharpGrabber;
using DotNetTools.SharpGrabber.Grabbed;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AllinTool.API.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class YoutubeDownloaderController : ControllerBase
        {
        private readonly IWebHostEnvironment webHostEnvironment;
        private static readonly HttpClient _client = new HttpClient();

        public YoutubeDownloaderController(IWebHostEnvironment webHostEnvironment)
            {
            this.webHostEnvironment = webHostEnvironment;
            }

        [HttpGet]
        public async Task<IActionResult> Get(string Url)
            {
            var grabber = GrabberBuilder.New()
                .UseDefaultServices()
                .AddYouTube()
                .Build();

            var result = await grabber.GrabAsync(new Uri(Url));
            var info = result.Resource<GrabbedInfo>();
            Console.WriteLine("Time Length: {0}", info.Length);
            var images = result.Resources<GrabbedImage>();
            var videos = result.Resources<GrabbedMedia>();
            return Ok(result);
            }

        private async Task SingleDownload(Uri uri, string outputPath, GrabResult _grabResult, string downloadingText = "Downloading {0}...")
            {
            try
                {
                using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                    await SingleDownload(uri, outputStream, _grabResult, downloadingText);
                    }
                }
            catch (Exception ex)
                {
                // Log the exception
                Console.WriteLine(ex.Message);
                }
            }

        private async Task SingleDownload(Uri uri, Stream outputStream, GrabResult _grabResult, string downloadingText = "Downloading {0}...")
            {
            try
                {
                var response = await _client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);

                response.EnsureSuccessStatusCode();
                var totalBytes = response.Content.Headers.ContentLength;
                if (totalBytes == 0)
                    throw new Exception("No data to download.");

                var remainingBytes = totalBytes;

                var originalStream = await response.Content.ReadAsStreamAsync();
                var stream = await _grabResult.WrapStreamAsync(originalStream);
                var buffer = new byte[4096];
                while (true)
                    {
                    var countToRead = (int) Math.Min(remainingBytes ?? int.MaxValue, buffer.Length);
                    var read = await stream.ReadAsync(buffer, 0, countToRead);
                    if (read <= 0)
                        break;
                    await outputStream.WriteAsync(buffer, 0, read);
                    }
                }
            catch (Exception ex)
                {
                // Log the exception
                Console.WriteLine(ex.Message);
                }
            }

        [HttpGet("download/{fileName}")]
        public async Task<IActionResult> Download(string fileName)
            {
            var filePath = Path.Combine(webHostEnvironment.WebRootPath, "videos", fileName); // videos subfolder assumed

            if (!System.IO.File.Exists(filePath))
                {
                return NotFound("File not found");
                }

            var memoryStream = new MemoryStream(await System.IO.File.ReadAllBytesAsync(filePath));

            return File(memoryStream, "application/octet-stream", fileName);
            }

        private string CreateFilePathFolder()
            {
            try
                {
                string FilePath = Path.Combine(webHostEnvironment.WebRootPath, "videos");

                if (!Directory.Exists(FilePath))
                    {
                    Directory.CreateDirectory(FilePath);
                    }
                return FilePath;
                }
            catch (Exception ex)
                {
                // Log the exception
                Console.WriteLine(ex.Message);
                throw;
                }
            }

        [HttpGet("GetVideoResource")]
        public async Task<List<VideoResourceListModel>> GetVideoResource(string urlLink)
            {
            string Image = "";
            List<VideoResourceListModel> videoResourceListModels = new List<VideoResourceListModel>();

            var grabber = GrabberBuilder.New()
              .UseDefaultServices()
              .AddYouTube()
              .Build();

            var result = await grabber.GrabAsync(new Uri(urlLink));

            var info = result.Resource<GrabbedInfo>();
            var images = result.Resources<GrabbedImage>();

            foreach (var item in images)
                {
                if (item.ResourceUri.AbsoluteUri.Contains("mqdefault.jpg"))
                    {
                    Image = item.ResourceUri.AbsoluteUri;
                    break;
                    }
                else
                    {
                    Image = "";
                    }
                }

            TimeSpan durations = info.Length.Value;

            // Format the duration string based on the TimeSpan
            string duration;
            if (durations.Hours > 0)
                {
                // Include hours in the format if they are present
                duration = durations.ToString(@"hh\:mm\:ss");
                }
            else
                {
                // Exclude hours from the format if they are zero
                duration = durations.ToString(@"mm\:ss");
                }
            var videos = result.Resources<GrabbedMedia>();
            var lstVideo = videos.GroupBy(x => x.Resolution).Select(x => x.FirstOrDefault()).ToList();
            if (lstVideo.Any())
                {
                foreach (var item in lstVideo)
                    {
                    VideoResourceListModel videoResourceModel = new VideoResourceListModel();
                    if (item.Container.Contains("mp4"))
                        {
                        if (item.Resolution == Resolution.Low || item.Resolution == Resolution.Medium || item.Resolution == Resolution.Good || item.Resolution == Resolution.Better || item.Resolution == Resolution.Best)
                            {
                            videoResourceModel.Id = Guid.NewGuid();
                            videoResourceModel.Title = result.Title;
                            videoResourceModel.Description = result.Description;
                            videoResourceModel.CreationDate = result.CreationDate.ToString();
                            videoResourceModel.Author = info.Author;
                            videoResourceModel.Image = Image;
                            videoResourceModel.Duration = duration;
                            videoResourceModel.Container = item.Container;
                            videoResourceModel.FormatTitle = item.FormatTitle;
                            videoResourceModel.Resolution = item.Resolution;
                            videoResourceModel.ResourceUri = item.ResourceUri;
                            videoResourceListModels.Add(videoResourceModel);
                            }
                        }
                    }
                }
            return videoResourceListModels;
            }

        [HttpGet("DownloadVideo")]
        public async Task<IActionResult> DownloadVideo(string resourceUri)
            {
            using (var client = new HttpClient())
                {
                var response = await client.GetAsync(resourceUri);

                if (response.IsSuccessStatusCode)
                    {
                    var fileBytes = await response.Content.ReadAsByteArrayAsync();

                    return File(fileBytes, "application/octet-stream", "video.mp4");
                    }
                else
                    {
                    return NotFound();
                    }
                }
            }

        [HttpGet("GetVideoByResourceUri")]
        public async Task<string> DownloadVideoByResolution(string urlLink, Uri ResourceUri)
            {
            var folderPath = CreateFilePathFolder();

            var grabber = GrabberBuilder.New()
              .UseDefaultServices()
              .AddYouTube()
              .Build();

            var result = await grabber.GrabAsync(new Uri(urlLink));
            string x = $"{result.Title}.mp4";
            var y = x.Split(" ");
            var z = string.Join("_", y);
            string FilePath = Path.Combine(folderPath, z);

            if (FilePath.Contains("|"))
                {
                FilePath = FilePath.Replace("|", "-");
                }

            await SingleDownload(ResourceUri, FilePath, result);

            return FilePath;
            }
        }
    }
