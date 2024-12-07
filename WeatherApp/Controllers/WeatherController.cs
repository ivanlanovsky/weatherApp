using Microsoft.AspNetCore.Mvc;
using System.Net;
using WeatherApp.Exceptions;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly ILogger<WeatherController> _logger;
        private readonly IWeatherService _weatherService;

        public WeatherController(WeatherService weatherService, ILogger<WeatherController> logger)
        {
            _logger = logger;
            _weatherService = weatherService;
        }

        [HttpGet("{city}")]
        public async Task<IActionResult> Get(string city)
        {
            try
            {
                var weatherData = await _weatherService.GetWeatherDataAsync(city);
                return Ok(weatherData);
            }
            catch (ApiErrorException e)
            {
                if(e.Response.StatusCode == HttpStatusCode.NotFound)
                {
                    return NotFound(new { message = $"Weather data for city '{city}' was not found." });
                }
                return BadRequest(new { error = $"{e.Message}: {e.Response.ReasonPhrase}" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
