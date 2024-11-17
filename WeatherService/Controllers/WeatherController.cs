using Microsoft.AspNetCore.Mvc;

namespace WeatherService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly WeatherService _weatherService;

    public WeatherController(WeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet]
    public async Task<IActionResult> GetWeather()
    {
        var weatherData = await _weatherService.GetWeatherDataAsync();
        if (weatherData == null)
            return NoContent();

        return Ok(weatherData);
    }
}
