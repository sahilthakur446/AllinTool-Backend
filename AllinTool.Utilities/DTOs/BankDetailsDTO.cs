using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinTool.Utilities.DTOs
    {
    public class BankDetailsDTO
        {
        public string? Bank { get; set; }

        public string Ifsc { get; set; } = null!;

        public string? Branch { get; set; }

        public string? Centre { get; set; }

        public string? District { get; set; }

        public string? State { get; set; }

        public string? Address { get; set; }
        public string? City { get; set; }
        }
    }
