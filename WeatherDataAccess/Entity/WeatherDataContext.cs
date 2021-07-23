using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApi.Models;

namespace WeatherApi.Entity
{
    public class WeatherDataContext : DbContext
    {
        public WeatherDataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<WeatherData> WeatherDatas { get; set; }
    }
}
