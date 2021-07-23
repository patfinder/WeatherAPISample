using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WeatherApi.Models;

namespace WeatherApi.Mapper
{
    /// <summary>
    /// Helper class that help CsvHelper to map date_time_local field (which is a date-time field in custom format)
    /// </summary>
    public sealed class CsvWeatherDataMap : ClassMap<WeatherData>
    {
        public CsvWeatherDataMap()
        {
            AutoMap(CultureInfo.InvariantCulture);

            Map(o => o.date_time_local).Convert(row =>
            {
                try
                {
                    // Read date_time_local string value
                    var date_time_localStr = row.Row.GetField<string>("date_time_local");
                    // Convert to DateTime. Substring to remove trailing CDT ("2021-07-22 11:00:00 CDT")
                    var dateTime = DateTime.ParseExact(date_time_localStr.Substring(0, 19), "yyyy-MM-dd HH:mm:ss", null);

                    return dateTime;
                }
                catch(Exception /*ex*/)
                {
                    // TODO: log
                    return default(DateTime);
                }
            });
        }
    }
}
