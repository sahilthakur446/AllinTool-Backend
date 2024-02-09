using AllinTool.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinTool.Data.Repository.Interfaces
    {
    public interface IBankDetailRepository
        {
        Task<BankDetailsDTO> GetIFSCAsync(string bankName, string branchName);
        Task<BankDetailsDTO> GetBankDetailsAsync(string ifscCode);
        }
    }
