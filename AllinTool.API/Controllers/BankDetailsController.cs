using AllinTool.Data.Repository.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AllinTool.Data.Repository.Interfaces;

namespace AllinTool.API.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class BankDetailsController : ControllerBase
        {
        private readonly IBankDetailRepository _bankDetailRepository;

        public BankDetailsController(IBankDetailRepository bankDetailRepository)
            {
            _bankDetailRepository = bankDetailRepository;
            }

        //[HttpGet("{bank}/{branch}")]
        [HttpGet("GetIFSC")]
        public async Task<IActionResult> GetIFSC(string bank, string branch)
            {
            var bankDetailsDto = await _bankDetailRepository.GetIFSCAsync(bank, branch);
            return Ok(bankDetailsDto);
            }

        [HttpGet("{ifscCode}")]
        public async Task<IActionResult> GetBankDetails(string ifscCode)
            {
            var bankDetailsDto = await _bankDetailRepository.GetBankDetailsAsync(ifscCode);
            return Ok(bankDetailsDto);
            }
        }
    }
