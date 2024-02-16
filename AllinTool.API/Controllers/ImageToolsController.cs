using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SixLabors.ImageSharp;

using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Tiff;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Bmp;
using System.Text;

namespace AllinTool.API.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class ImageToolsController : ControllerBase
        {
        [HttpPost("upload")]
        public async Task<IActionResult> UploadImageAsync(IFormFile image, string format)
            {
            try
                {
                // Check if the uploaded file is an image
                if (image.ContentType.StartsWith("image"))
                    {
                    // Open the image using ImageSharp
                    using (var imageStream = new MemoryStream())
                        {
                        await image.CopyToAsync(imageStream);
                        imageStream.Seek(0, SeekOrigin.Begin);

                        using (var img = Image.Load(imageStream))
                            {
                            // Convert the image to the specified format
                            using (var outputStream = new MemoryStream())
                                {
                                string mimeType = "";
                                var fileName = new StringBuilder(Path.GetFileNameWithoutExtension(image.FileName));

                                switch (format)
                                    {
                                    case "png":
                                        img.Save(outputStream, new PngEncoder());
                                        mimeType = "image/png";
                                        fileName.Append(".png");
                                        break;
                                    case "jpeg":
                                        img.Save(outputStream, new JpegEncoder());
                                        mimeType = "image/jpeg";
                                        fileName.Append(".jpeg");
                                        break;
                                    case "tiff":
                                        img.Save(outputStream, new TiffEncoder());
                                        mimeType = "image/tiff";
                                        fileName.Append(".tiff");
                                        break;

                                    case "gif":
                                        img.Save(outputStream, new GifEncoder());
                                        mimeType = "image/gif";
                                        fileName.Append(".gif");
                                        break;
                                    case "bmp":
                                        img.Save(outputStream, new BmpEncoder());
                                        mimeType = "image/bmp";
                                        fileName.Append(".bmp");
                                        break;


                                    }

                                outputStream.Seek(0, SeekOrigin.Begin);

                                // Return the converted image to the client
                                return File(outputStream.ToArray(), mimeType, fileName.ToString());
                                }
                            }
                        }
                    }
                else
                    {
                    return BadRequest("Uploaded file is not an image.");
                    }
                }
            catch (Exception ex)
                {
                return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }


        }
    }
