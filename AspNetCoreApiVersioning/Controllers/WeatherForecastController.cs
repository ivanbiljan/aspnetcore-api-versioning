using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreApiVersioning.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public partial class WeatherForecastController : ControllerBase
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

    [MapToApiVersion("1.0")]
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

    [HttpGet("noversion")]
    public IActionResult RemainsUnversioned()
    {
        return Ok("Unversioned endpoint");
    }
}

[ApiVersion("2.0")]
public partial class WeatherForecastController
{
    [MapToApiVersion("2.0")]
    [HttpGet(Name = "GetWeatherForecast")]
    public IActionResult GetV2()
    {
        return Ok("V2");
    }
}