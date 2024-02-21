using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Tiff;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Processing;
using System.Net;
using System.Text;
using System.Runtime.InteropServices;


namespace AllinTool.API.Controllers
    {
    
    public class ImageData
        {
        public string MimeType { get; set; }
        public StringBuilder FileName { get; set; }
        public Image InputImage { get; set; }
        public MemoryStream OutputStream { get; set; } = new MemoryStream();
        }
    [Route("api/[controller]")]
    [ApiController]
    public class ImageToolsController : ControllerBase
        {
        private ImageData SaveImage(ImageData imageData, string targetFormat)
            {
            switch (targetFormat)
                {
                case "png":
                    imageData.InputImage.Save(imageData.OutputStream, new PngEncoder());
                    imageData.MimeType = "image/png";
                    imageData.FileName.Append(".png");
                    break;
                case "jpeg":
                    imageData.InputImage.Save(imageData.OutputStream, new JpegEncoder());
                    imageData.MimeType = "image/jpeg";
                    imageData.FileName.Append(".jpeg");
                    break;
                case "tiff":
                    imageData.InputImage.Save(imageData.OutputStream, new TiffEncoder());
                    imageData.MimeType = "image/tiff";
                    imageData.FileName.Append(".tiff");
                    break;
                case "gif":
                    imageData.InputImage.Save(imageData.OutputStream, new GifEncoder());
                    imageData.MimeType = "image/gif";
                    imageData.FileName.Append(".gif");
                    break;
                case "bmp":
                    imageData.InputImage.Save(imageData.OutputStream, new BmpEncoder());
                    imageData.MimeType = "image/bmp";
                    imageData.FileName.Append(".bmp");
                    break;
                default:
                    throw new ArgumentException("Invalid format selected");
                }
            return imageData;
            }

        private async Task<ImageData> ProcessImage(IFormFile image, string targetFormat)
            {
            if (!image.ContentType.StartsWith("image"))
                {
                throw new ArgumentException("Uploaded file is not an image");
                }

            var imageStream = new MemoryStream();
            await image.CopyToAsync(imageStream);
            imageStream.Seek(0, SeekOrigin.Begin);
            var img = Image.Load(imageStream);

            var outputStream = new MemoryStream();
            var mimeType = "";
            var fileName = new StringBuilder(Path.GetFileNameWithoutExtension(image.FileName));

            var imageData = new ImageData()
                {
                MimeType = mimeType,
                FileName = fileName,
                InputImage = img,
                OutputStream = outputStream
                };

            return SaveImage(imageData, targetFormat);
            }

        private async Task<ImageData> ProcessImage(IFormFile image, string? targetFormat, int? width, int? height)
            {
            if (!image.ContentType.StartsWith("image"))
                {
                throw new ArgumentException("Uploaded file is not an image");
                }

            var imageStream = new MemoryStream();
            await image.CopyToAsync(imageStream);
            imageStream.Seek(0, SeekOrigin.Begin);
            var img = Image.Load(imageStream);

            img.Mutate(x => x.Resize(new ResizeOptions
                {
                Size = new Size((int) width, (int) height),
                Mode = ResizeMode.Max
                }));

            var outputStream = new MemoryStream();
            var mimeType = "";
            var fileName = new StringBuilder(Path.GetFileNameWithoutExtension(image.FileName));

            var imageContentTypeArray = image.ContentType.Split('/');
            Array.Reverse(imageContentTypeArray);

            if (String.IsNullOrEmpty(targetFormat))
                {
                targetFormat = imageContentTypeArray[0];
                }

            var imageData = new ImageData()
                {
                MimeType = mimeType,
                FileName = fileName,
                InputImage = img,
                OutputStream = outputStream
                };

            return SaveImage(imageData, targetFormat);
            }

        [HttpPost("upload")]
        public async Task<IActionResult> ConvertImageAsync(IFormFile image, string targetFormat)
            {
            var imageFormatArray = image.ContentType.Split('/');
            Array.Reverse(imageFormatArray);

            if (String.IsNullOrEmpty(targetFormat))
                {
                return BadRequest("TargetFormat is not selected");
                }

            if (targetFormat == imageFormatArray[0])
                {
                return BadRequest("Original format and target format of the image are the same");
                }

           
                var processedImageData = await ProcessImage(image, targetFormat);
                processedImageData.OutputStream.Seek(0, SeekOrigin.Begin);
                return File(processedImageData.OutputStream.ToArray(), processedImageData.MimeType, processedImageData.FileName.ToString());
                
           
                return StatusCode((int) HttpStatusCode.InternalServerError, $"Internal server error:");
                
            }

        [HttpPost("convertsize")]
        public async Task<IActionResult> ResizeImage(IFormFile image, [Optional] int? height, [Optional] int width, [Optional] string? targetFormat)
            {
            try
                {
                
                var processedImageData = await ProcessImage(image, targetFormat, width, height);
                processedImageData.OutputStream.Seek(0, SeekOrigin.Begin);
                return File(processedImageData.OutputStream.ToArray(), processedImageData.MimeType, processedImageData.FileName.ToString());
                }
            catch (Exception ex)
                {
                return StatusCode((int) HttpStatusCode.InternalServerError, $"Internal server error: {ex.Message}");
                }
            }
        }
    }
