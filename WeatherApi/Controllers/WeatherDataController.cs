using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApi.Dto;
using WeatherApi.Entity;
using WeatherApi.Models;

namespace WeatherApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherDataController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<WeatherDataController> _logger;

        private const string SERVER_ERROR = "Server error";

        private WeatherDataContext _weatherDataContext { get; }

        public WeatherDataController(WeatherDataContext weatherDataContext, IMapper mapper, ILogger<WeatherDataController> logger)
        {
            this._weatherDataContext = weatherDataContext;
            this._mapper = mapper;
            this._logger = logger;
        }

        /// <summary>
        /// Get weather data for one date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet("{date}")]
        public ObjectResult Get(string date)
        {
            try
            {
                //DateTime dateDt = DateTime.Parse(date);
                DateTime date1 = DateTime.Parse(date).Date;
                var date2 = date1.AddDays(1);

                var data = _weatherDataContext.WeatherDatas
                    .Where(d => d.date_time_local >= date1 && d.date_time_local < date2).ToList();

                // Map to DTO
                return Ok(data.ConvertAll(d => _mapper.Map<WeatherDataDto>(d)));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem(SERVER_ERROR);
            }
        }

        /// <summary>
        /// Get weather data for one hour.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="hour"></param>
        /// <returns></returns>
        [HttpGet("{date}/{hour}")]
        public ObjectResult Get(string date, string hour)
        {
            try
            {
                int time1 = int.Parse(hour);
                var date1 = DateTime.Parse(date).Date.AddHours(time1);

                var data = _weatherDataContext.WeatherDatas
                    .FirstOrDefault(d => d.date_time_local == date1);

                // Map to DTO
                return Ok(_mapper.Map<WeatherDataDto>(data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem(SERVER_ERROR);
            }
        }

        /// <summary>
        /// Create new entry
        /// </summary>
        /// <param name="weatherData"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(WeatherData weatherData)
        {
            try
            {
                await _weatherDataContext.AddAsync(weatherData);
                _weatherDataContext.SaveChanges();

                return Ok(new { count = 1});
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem(SERVER_ERROR);
            }
        }
    }
}
