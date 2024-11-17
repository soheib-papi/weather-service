using Microsoft.EntityFrameworkCore;

namespace WeatherService;

public class WeatherService
{
    private readonly HttpClient _httpClient;
    private readonly WeatherDbContext _dbContext;
    private const string WeatherApiUrl = "https://api.openmeteo.com/v1/forecast?latitude=52.52&longitude=13.41&hourly=temperature_2m";

    public WeatherService(HttpClient httpClient, WeatherDbContext dbContext)
    {
        _httpClient = httpClient;
        _dbContext = dbContext;
    }

    public async Task<WeatherData?> GetWeatherDataAsync()
    {
        // تلاش برای دریافت اطلاعات از API
        var response = await _httpClient.GetAsync(WeatherApiUrl);
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadFromJsonAsync<WeatherApiResponse>();
            if (data != null && data.Hourly.Temperature2M.Any())
            {
                var latestTemperature = data.Hourly.Temperature2M.First();
                var weatherData = new WeatherData
                {
                    Timestamp = DateTime.UtcNow,
                    Temperature = latestTemperature
                };

                // ذخیره در دیتابیس
                _dbContext.WeatherRecords.Add(weatherData);
                await _dbContext.SaveChangesAsync();

                return weatherData;
            }
        }

        // بازگرداندن آخرین داده ذخیره‌شده در صورت شکست
        return await _dbContext.WeatherRecords.OrderByDescending(w => w.Timestamp).FirstOrDefaultAsync();
    }
}
