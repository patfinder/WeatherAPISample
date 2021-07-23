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
    public sealed class CsvWeatherDataMap : ClassMap<WeatherDataCsv>
    {
        public CsvWeatherDataMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(o => o.date_time_localCsv).Name("date_time_local");
            Map(m => m.date_time_local).Ignore();
        }
    }
}
