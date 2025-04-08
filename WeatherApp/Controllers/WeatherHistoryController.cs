using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WeatherApp.Services;
using WeatherApp.DbContexts;
using Microsoft.EntityFrameworkCore;
using WeatherApp.DTO;
using WeatherApp.Common;

namespace WeatherApp.Controllers
{
    [Authorize(policy: StringConstants.AdminPolicyTitle)]
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherHistoryController : ControllerBase
    {
        private readonly ILogger<WeatherController> _logger;
        private readonly WeatherDbContext _dbContext;

        public WeatherHistoryController(WeatherDbContext dbContext, ILogger<WeatherController> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("{count}")]
        public async Task<IActionResult> Get(int count)
        {
            try
            {
                var weatherData = _dbContext.WeatherHistory
                    .Include(w => w.WeatherData)
                    .OrderByDescending(w => w.Date).Take(count)
                    .Select(w => new WeatherHistoryData()
                    {
                        City = w.City,
                        Date = w.Date,
                        WeatherData = $"Temp:{w.WeatherData.Temperature}, hum:{w.WeatherData.Humidity}, desc:{w.WeatherData.Description}"
                    })
                    .ToList();
                return Ok(weatherData);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
