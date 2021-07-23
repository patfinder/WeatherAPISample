using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApi.Dto
{
    //[AutoMap(typeof(WeatherData))]
    public class WeatherDataDto
    {
        public int unixtime { get; set; }
        public DateTime date_time_local { get; set; }
        public decimal? pressure_station { get; set; }
        public decimal? pressure_sea { get; set; }
        public string wind_dir { get; set; }
        public int? wind_dir_10s { get; set; }
        public int? wind_speed { get; set; }
        public int? wind_gust { get; set; }
        public int? relative_humidity { get; set; }
        public decimal? dew_point { get; set; }
        public decimal? temperature { get; set; }
        public decimal? windchill { get; set; }
        public int? humidex { get; set; }
        public int? visibility { get; set; }
        public decimal? health_index { get; set; }
        public decimal? cloud_cover_4 { get; set; }
        public int? cloud_cover_8 { get; set; }
        public int? cloud_cover_10 { get; set; }
        public decimal? solar_radiation { get; set; }
        public decimal? max_air_temp_pst1hr { get; set; }
        public decimal? min_air_temp_pst1hr { get; set; }
    }
}
