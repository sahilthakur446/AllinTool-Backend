using AllinTool.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinTool.Data.Repository.Interfaces
    {
    public interface ITimezoneConverter
        
        {
        string ConvertTime(string fromtimezone, string totimezone, string date);
        Task<List<TimeZoneDTO>> FetchTimeZones();
        }
    }
