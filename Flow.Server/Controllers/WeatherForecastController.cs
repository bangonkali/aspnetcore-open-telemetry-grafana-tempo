using Microsoft.AspNetCore.Mvc;

namespace Flow.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        using (var activity = DiagnosticsConfig.ActivitySource.StartActivity("activity1"))
        {
            if (activity != null)
            {
                activity.SetTag("start", 1);
                await Task.Delay(1000);
                
                activity.SetTag("stop", "Hello, World!");
                activity.SetTag("baz", new int[] { 1, 2, 3 });
                activity.Stop();
            }
        }
        
        
        using (var activity2 = DiagnosticsConfig.ActivitySource.StartActivity("activity2"))
        {
            if (activity2 != null)
            {
                activity2.SetTag("start", 1);
                await Task.Delay(1000);
                
                activity2.SetTag("stop", "Hello, World!");
                activity2.SetTag("baz", new int[] { 1, 2, 3 });
                activity2.Stop();
            }
        }
        
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}