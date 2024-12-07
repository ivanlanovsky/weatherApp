using Microsoft.AspNetCore.Mvc;
using WeatherApp.Services;
using WeatherApp.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace WeatherApp.Controllers
{
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
                    .OrderByDescending(w => w.Date).Take(count).ToList();
                return Ok(weatherData);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
