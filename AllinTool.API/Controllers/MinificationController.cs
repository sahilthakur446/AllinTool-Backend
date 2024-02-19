using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUglify;
using System;
using System.Net;

namespace AllinTool.API.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class MinifyController : ControllerBase
        {
        [HttpPost("js")]
        public ActionResult<string> MinifyJs([FromBody] string jsCode)
            {
            try
                {
                // Minify JavaScript code
                var result = Uglify.Js(jsCode);

                if (result.HasErrors)
                    {
                    // Return BadRequest with error details if minification fails
                    return BadRequest(new { errors = result.Errors });
                    }

                // Return the minified JavaScript code
                return Ok(new { convertedCode = result.Code });
                }
            catch (Exception ex)
                {
                // Return InternalServerError with the exception message
                return StatusCode((int) HttpStatusCode.InternalServerError, new { error = ex.Message });
                }
            }

        [HttpPost("css")]
        public ActionResult<string> MinifyCss([FromBody] string cssCode)
            {
            try
                {
                // Minify CSS code
                var result = Uglify.Css(cssCode);

                if (result.HasErrors)
                    {
                    // Return BadRequest with error details if minification fails
                    return BadRequest(new { errors = result.Errors });
                    }

                // Return the minified CSS code
                return Ok(new { convertedCode = result.Code });
                }
            catch (Exception ex)
                {
                // Return InternalServerError with the exception message
                return StatusCode((int) HttpStatusCode.InternalServerError, new { error = ex.Message });
                }
            }
        }
    }
