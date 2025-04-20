using Microsoft.AspNetCore.Mvc;

namespace KasiCornerKota_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherForecastService _weatherForecastService;
    public WeatherForecastController(ILogger<WeatherForecastController> logger,IWeatherForecastService weatherForecastService)
    {
        _logger = logger;
        _weatherForecastService = weatherForecastService;
    }

    [HttpGet]
    [Route("{take}/example")]
    public IEnumerable<WeatherForecastModel> Get([FromQuery]int max, [FromRoute]int take)
    {
        var results = _weatherForecastService.Get();
        return results;
    }

    [HttpGet("currentDay")]
    public WeatherForecastModel GetCurrentDay()
    {
        var result = _weatherForecastService.Get().First();
        return result;
    }
    [HttpPost]
    public string Hello([FromBody] string name)
    {
        return $"Hello {name}";
    }
}