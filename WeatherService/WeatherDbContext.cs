using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherService;

public class WeatherDbContext : DbContext
{
    public DbSet<WeatherData> WeatherRecords { get; set; }

    public WeatherDbContext(DbContextOptions<WeatherDbContext> options)
        : base(options)
    {
    }
}
