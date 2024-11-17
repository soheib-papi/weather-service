namespace WeatherService;

public class WeatherApiResponse
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double GenerationtimeMs { get; set; }
    public int UtcOffsetSeconds { get; set; }
    public string Timezone { get; set; }
    public string TimezoneAbbreviation { get; set; }
    public double Elevation { get; set; }
    public HourlyUnits HourlyUnits { get; set; }
    public HourlyData Hourly { get; set; }
}

public class HourlyUnits
{
    public string Time { get; set; }
    public string Temperature2M { get; set; }
}

public class HourlyData
{
    public List<string> Time { get; set; }
    public List<double> Temperature2M { get; set; }
}
