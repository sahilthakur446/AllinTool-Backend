using AllinTool.Data.Context;
using AllinTool.Data.Repository.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AllinTool.API.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class GeographicDataController : ControllerBase
        {
        private readonly IGeographicRepository _geographicRepository;

        public GeographicDataController(IGeographicRepository geographicRepository)
            {
            _geographicRepository = geographicRepository;
            }

        [HttpGet("GetByPostalCode")]
        public async Task<IActionResult> GetByPostalCode(string postalcode)
            {
            var data = await _geographicRepository.GetByPostalCode(postalcode);
            return Ok(data);
            }

        [HttpGet("GetByLocation")]
        public async Task<IActionResult> GetByLocation(string state, string city, string country)
            {
            var data = await _geographicRepository.GetByLocation(state, city, country);
            return Ok(data);
            }
        }
    }
