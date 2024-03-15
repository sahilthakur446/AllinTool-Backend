using AllinTool.Data.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text;

namespace AllinTool.API.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class SqlConversionController : ControllerBase
        {
        private readonly ISQLConversionRepository sqlconversionRepo;

        public SqlConversionController(ISQLConversionRepository _sqlconversionRepo)
        {
            sqlconversionRepo = _sqlconversionRepo;
        
        }


        [HttpPost("ConvertToMSSQL")]
        public IActionResult ConvertToMSSQL([FromBody] string query)
            {
            if (string.IsNullOrEmpty(query))
                {
                return BadRequest("Query is empty or null.");
                }

            try
                {
                string adjustedQuery = sqlconversionRepo.ConvertMYSQLQueryTOMSSQLQuery(query);

                return Ok(new { adjustedQuery = adjustedQuery });
                }
            catch (Exception ex)
                {
                return BadRequest(ex.Message);
                }
            }

        [HttpPost("ConvertToMYSQL")]
        public IActionResult ConvertToMYSQL([FromBody] string query)
            {
            if (string.IsNullOrEmpty(query))
                {
                return BadRequest("Query is empty or null.");
                }

            try
                {
                string adjustedQuery = sqlconversionRepo.ConvertMSSQLQueryToMYSQLQuery(query);
               

                return Ok(new { adjustedQuery = adjustedQuery });
                }
            catch (Exception ex)
                {
                return BadRequest(ex.Message);
                }
            }

        }
    }