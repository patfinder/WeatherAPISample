using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WeatherApi.Entity;
using WeatherApi.Mapper;
using WeatherApi.Models;

namespace WeatherApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadDataController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<WeatherDataController> _logger;

        private WeatherDataContext _weatherDataContext { get; }

        /// <summary>
        /// Upload weather data
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="weatherDataContext"></param>
        public UploadDataController(WeatherDataContext weatherDataContext, IMapper mapper, ILogger<WeatherDataController> logger)
        {
            this._weatherDataContext = weatherDataContext;
            this._mapper = mapper;
            this._logger = logger;
        }

        /// <summary>
        /// Upload CSV weather data files.
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostUploadAsync([FromForm] List<IFormFile> files)
        {
            int FileCount = files.Count, RowCount = 0;

            long size = files.Sum(f => f.Length);
            List<string> fileList = new List<string>(files.Count);

            try
            {
                // Save upload file to temp
                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                        var filePath = Path.GetTempFileName();
                        fileList.Add(filePath);

                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                    }
                }

                // Csv config
                var csvConfiguration = new CsvConfiguration(CultureInfo.CurrentCulture) { 
                    HasHeaderRecord = true,
                    BadDataFound = context => {
                        _logger.LogError($"Field: {context.Field} - Value: {context.RawRecord}");
                    },
                };

                // Read files and import to DB
                foreach (var filePath in fileList)
                {
                    using var reader = new StreamReader(filePath);
                    using var csvReader = new CsvReader(reader, csvConfiguration);
                    csvReader.Context.RegisterClassMap<CsvWeatherDataMap>();

                    var weatherData = csvReader.GetRecords<WeatherData>().ToList();

                    // row count
                    RowCount += weatherData.Count;

                    await _weatherDataContext.AddRangeAsync(weatherData);
                    await _weatherDataContext.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            
            return Ok(new { FileCount, RowCount });
        }
    }
}
