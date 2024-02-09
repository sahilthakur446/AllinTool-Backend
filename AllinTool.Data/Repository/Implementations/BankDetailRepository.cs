using AllinTool.Data.Context;
using AllinTool.Data.Repository.Interfaces;
using AllinTool.Utilities.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinTool.Data.Repository.Implementations
    {
    public class BankDetailRepository : IBankDetailRepository
        {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BankDetailRepository(ApplicationDbContext context, IMapper mapper)
            {
            _context = context;
            _mapper = mapper;
            }

        public async Task<BankDetailsDTO> GetBankDetailsAsync(string ifscCode)
            {
            var bankDetails = await _context.BankDetails.FindAsync(ifscCode);
            return bankDetails == null ? null : _mapper.Map<BankDetailsDTO>(bankDetails);
            }

        public async Task<BankDetailsDTO> GetIFSCAsync(string bankName, string branchName)
            {
            var bankDetails = await _context.BankDetails.FirstOrDefaultAsync(b => b.Bank == bankName && b.Branch == branchName);
            return bankDetails == null ? null : _mapper.Map<BankDetailsDTO>(bankDetails);
            }
        }
    }
