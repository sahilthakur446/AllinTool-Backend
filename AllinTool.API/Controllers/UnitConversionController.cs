using AllinTool.Data.Repository.Abstract;
using AllinTool.Data.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AllinTool.API.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class UnitConversionController : ControllerBase
        {
        private readonly IUnitConverter converter;
        private readonly ITimezoneConverter timezoneConverter;

        public UnitConversionController(IUnitConverter converter,ITimezoneConverter timezoneConverter)
            {
            this.converter = converter;
            this.timezoneConverter = timezoneConverter;
            }
        [HttpPost]
        public IActionResult ConvertUnits(string from, string to, double input, string unitType)
            {

            var x = converter.ConvertUnits(from, to, input, unitType);
            if (x == false)
                {
                return BadRequest();
                }
            return Ok(new { result = converter.ConvertedValue, unit = to });

            }
        [HttpPost("ConvertTime")]
        public IActionResult ConvertTime(string from = "Asia/Kolkata", string to = "America/Los_Angeles", string date = "")
            {
            var convertedTime = timezoneConverter.ConvertTime(from, to, date);
            return Ok(new { ConvertedTime = convertedTime });
            }
        [HttpGet("AvailableTimeZones")]
        public  IActionResult FetchTimeZones()
            {
            var availableTimeZones = timezoneConverter.FetchTimeZones();
            return Ok(availableTimeZones);
            }
        }
    }
