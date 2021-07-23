using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using WeatherApi.Controllers;
using WeatherApi.Dto;
using WeatherApi.Entity;
using WeatherApi.Mapper;
using WeatherApi.Models;

namespace WeatherApi.Test
{
    [TestFixture]
    public class WeatherDataControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestGetForDate()
        {
            // Mapper
            var mapper = GetMapperConfig().CreateMapper();

            // Arrange
            var mockLogger = new Mock<ILogger<WeatherDataController>>();

            // Db Context
            InitTestWeatherDataContext();
            using (var context = new WeatherDataContext(GetTestWeatherDataContextOptions()))
            {

                // Controller
                var controller = new WeatherDataController(context, mapper, mockLogger.Object);
                
                // Act
                var data = controller.Get("2021-07-22");
                var length = ((List<WeatherDataDto>)(data.Value)).Count;

                // Assert
                Assert.AreEqual(length, context.WeatherDatas.Count());
            }
        }

        private DbContextOptions<WeatherDataContext> GetTestWeatherDataContextOptions()
        {
            var options = new DbContextOptionsBuilder<WeatherDataContext>()
                .UseInMemoryDatabase(databaseName: "WeatherDataDb")
                .Options;

            return options;
        }

        private void InitTestWeatherDataContext()
        {
            var date_time_local = new DateTime(2021, 7, 22, 1, 1, 0, 0);

            // Insert seed data into the database using one instance of the context
            using (var context = new WeatherDataContext(GetTestWeatherDataContextOptions()))
            {
                context.WeatherDatas.Add(new WeatherData {
                    unixtime = 1626944400,
                    date_time_local = date_time_local,
                    pressure_station = 98.37m,
                    pressure_sea = 101.21m,
                    wind_dir = "ESE",
                    wind_dir_10s = 12,
                    wind_speed = 15,
                    wind_gust = null,
                    relative_humidity = 89,
                    dew_point = 17.4m,
                    temperature = 19.2m,
                    windchill = null,
                    humidex = null,
                    visibility = 9700,
                    health_index = null,
                    cloud_cover_4 = null,
                    cloud_cover_8 = 0,
                    cloud_cover_10 = null,
                    solar_radiation = null,
                    max_air_temp_pst1hr = 19.5m,
                    min_air_temp_pst1hr = 19.2m
                });
                context.WeatherDatas.Add(new WeatherData
                {
                    unixtime = 1626944401,
                    date_time_local = date_time_local,
                    pressure_station = 98.37m,
                    pressure_sea = 101.21m,
                    wind_dir = "ESE",
                    wind_dir_10s = 12,
                    wind_speed = 15,
                    wind_gust = null,
                    relative_humidity = 89,
                    dew_point = 17.4m,
                    temperature = 19.2m,
                    windchill = null,
                    humidex = null,
                    visibility = 9700,
                    health_index = null,
                    cloud_cover_4 = null,
                    cloud_cover_8 = 0,
                    cloud_cover_10 = null,
                    solar_radiation = null,
                    max_air_temp_pst1hr = 19.5m,
                    min_air_temp_pst1hr = 19.2m
                });
                context.WeatherDatas.Add(new WeatherData
                {
                    unixtime = 1626944402,
                    date_time_local = date_time_local,
                    pressure_station = 98.37m,
                    pressure_sea = 101.21m,
                    wind_dir = "ESE",
                    wind_dir_10s = 12,
                    wind_speed = 15,
                    wind_gust = null,
                    relative_humidity = 89,
                    dew_point = 17.4m,
                    temperature = 19.2m,
                    windchill = null,
                    humidex = null,
                    visibility = 9700,
                    health_index = null,
                    cloud_cover_4 = null,
                    cloud_cover_8 = 0,
                    cloud_cover_10 = null,
                    solar_radiation = null,
                    max_air_temp_pst1hr = 19.5m,
                    min_air_temp_pst1hr = 19.2m
                });
                context.SaveChanges();
            }
        }

        private MapperConfiguration GetMapperConfig()
        {
            var configuration = new MapperConfiguration(config => {
                config.CreateMap<WeatherDataCsv, WeatherData>()
                    .ForMember(d => d.date_time_local, opt => opt.MapFrom<WeatherDataResolver>());
                config.CreateMap<WeatherData, WeatherDataDto>();
            });

            return configuration;
        }
    }
}