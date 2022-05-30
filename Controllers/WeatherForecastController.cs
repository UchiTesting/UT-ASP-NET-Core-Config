using Microsoft.AspNetCore.Mvc;

namespace ASP.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly Microsoft.Extensions.Options.IOptions<MyApiOptions> _myApiOptions;

    public WeatherForecastController(ILogger<WeatherForecastController> logger,
        Microsoft.Extensions.Options.IOptions<MyApiOptions> myApiOptions)
    {
        _logger = logger;
        _myApiOptions = myApiOptions;
        var output = new System.Text.StringBuilder()
            .AppendLine("Injected custom options in WeatherForecastController with values:")
            .AppendLine($"- URL => {myApiOptions.Value.Url}")
            .AppendLine($"- API Key => {myApiOptions.Value.ApiKey}");

        Console.WriteLine(output.ToString());
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
