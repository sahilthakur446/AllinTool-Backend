using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinTool.Data.Repository.Abstract
    {
    public interface IUnitConverter
        {
        double ConvertedValue { get; set; }
        bool ConvertUnits(string from, string to, double input, string unitType);
        }
    }
