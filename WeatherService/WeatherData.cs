using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherService;

public class WeatherData
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; }
    public double Temperature { get; set; }
}
