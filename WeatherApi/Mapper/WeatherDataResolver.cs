using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApi.Models;

namespace WeatherApi.Mapper
{
    /// <summary>
    /// Helper class to support AutoMapper mapping value 
    /// from WeatherDataCsv.date_time_localCsv to WeatherData.date_time_local
    /// </summary>
    public class WeatherDataResolver : IValueResolver<WeatherDataCsv, WeatherData, DateTime>
    {
        public DateTime Resolve(WeatherDataCsv source, WeatherData dest, DateTime destMember, ResolutionContext context)
        {

            try
            {
                var dateTime = DateTime.ParseExact(source.date_time_localCsv.Substring(0, 19), "yyyy-MM-dd HH:mm:ss", null);

                // Offset for CDT
                //dateTime = dateTime.AddHours(-1);

                return dateTime;
            }
            catch (Exception) {
                // TODO: log
            }
            
            return default(DateTime);
        }
    }
}
