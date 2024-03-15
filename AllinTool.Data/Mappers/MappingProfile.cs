using AllinTool.Data.Models;
using AllinTool.Utilities.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinTool.Data.Mappers
    {
    public class MappingProfile: Profile
        {
            public MappingProfile()
                {
                
                CreateMap<GeographicData, GeographicDataDTO>();
                CreateMap<BankDetail, BankDetailsDTO>();
            CreateMap<List<string>, TimeZoneDTO>()
            .ForMember(dest => dest.Time_Zone, opt => opt.MapFrom(src => src));


            }
        }
    }
    