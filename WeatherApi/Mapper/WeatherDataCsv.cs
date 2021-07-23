using AutoMapper;
using AutoMapper.Configuration.Annotations;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApi.Models
{
    public class WeatherDataCsv: WeatherData
    {
        public string date_time_localCsv { get; set; }
    }
}
