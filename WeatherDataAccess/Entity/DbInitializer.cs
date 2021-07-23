using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApi.Entity
{
    public static class DbInitializer
    {
        public static void Initialize(WeatherDataContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.WeatherDatas.Any())
            {
                return;   // DB has been seeded
            }

            // TODO: add sample data

            context.SaveChanges();
        }
    }
}
