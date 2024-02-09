using AllinTool.Data.Context;
using AllinTool.Data.Models;
using AllinTool.Data.Repository.Abstract;
using AllinTool.Utilities.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllinTool.Data.Repository.Implementations
    {
    public class GeographicRepository : IGeographicRepository
        {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GeographicRepository(ApplicationDbContext dbContext, IMapper mapper)
            {
            _dbContext = dbContext;
            _mapper = mapper;
            }

        public async Task<GeographicDataDTO> GetByLocation(string state, string city, string country)
            {
            var data = await _dbContext.GeographicData.FirstOrDefaultAsync(x => x.State.ToLower() == state.ToLower() && x.Country.ToLower() == country.ToLower() && x.City.ToLower() == city.ToLower());
            return data == null ? null : _mapper.Map<GeographicDataDTO>(data);
            }

        public async Task<List<GeographicDataDTO>> GetByPostalCode(string postalCode)
            {
            var data = await _dbContext.GeographicData.Where(x => x.PostalCode == postalCode).ToListAsync();
            return ( data == null || !data.Any() ) ? null : _mapper.Map<List<GeographicDataDTO>>(data);
            }
        }
    }
