using AllinTool.Data.Context;
using AllinTool.Data.Mappers;
using AllinTool.Data.Repository.Interfaces;
using AllinTool.Utilities.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinTool.Data.Repository.Implementations
    {
    public class TimezoneConverter : ITimezoneConverter
        {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TimezoneConverter(ApplicationDbContext context,IMapper _mapper)
        {
            this.context = context;
            mapper = _mapper;
            }
        public string ConvertTime(string fromtimezone, string totimezone, string date)
            {
            // Find the time zone objects by their IDs
            TimeZoneInfo fromZone = TimeZoneInfo.FindSystemTimeZoneById(fromtimezone);
            TimeZoneInfo toZone = TimeZoneInfo.FindSystemTimeZoneById(totimezone);

            // Parse the date parameter as a DateTime object in the source time zone
            DateTime fromTime = DateTime.Parse(date, null, DateTimeStyles.AssumeLocal);
            fromTime = DateTime.SpecifyKind(fromTime, DateTimeKind.Unspecified);
            fromTime = TimeZoneInfo.ConvertTimeToUtc(fromTime, fromZone);

            // Convert the DateTime object to the destination time zone
            DateTime toTime = TimeZoneInfo.ConvertTime(fromTime, toZone);

            // Check if the converted time is on a different day
            if (toTime.Date != fromTime.Date)
                {
                string dayIndicator = toTime.Day > fromTime.Day ? " (on Next day)" : " (on Previous Day)";
                return toTime.ToString("dddd,MMM d,yyyy HH:mm:ss") + dayIndicator;
                }

            return toTime.ToString("dddd,MMM d,yyyy HH:mm:ss");
            }

        public  List<string> FetchTimeZones()
            {
            var timeZoneStrings =  context.TimeZoneTable
                                               .Select(x => x.Time_Zone)
                                               .Distinct()
                                               .ToList();

            var timeZoneDTOs =  mapper.Map<List<string>>(timeZoneStrings);
            return timeZoneDTOs;
            }
        
        }
    }
