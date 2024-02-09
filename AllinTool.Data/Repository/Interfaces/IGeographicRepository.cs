using AllinTool.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinTool.Data.Repository.Implementations
    {
    public interface IGeographicRepository
        {
        Task<List<GeographicDataDTO>> GetByPostalCode(string postalcode);
        Task<GeographicDataDTO> GetByLocation(string state, string city, string country);
        }
    }
