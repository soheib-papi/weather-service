using Microsoft.EntityFrameworkCore;

namespace WeatherService;

public class WeatherDbContext : DbContext
{
    public DbSet<WeatherData> WeatherRecords { get; set; }

    public WeatherDbContext(DbContextOptions<WeatherDbContext> options)
        : base(options)
    {
    }
}
