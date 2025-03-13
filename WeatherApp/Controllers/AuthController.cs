using Microsoft.AspNetCore.Mvc;
using System.Net;
using WeatherApp.DTO;
using WeatherApp.Exceptions;
using WeatherApp.Services;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;

        public AuthController(ILogger<AuthController> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Get(UserCredentials credentials)
        {
            try
            {
                var user = await _authService.Authenticate(credentials, HttpContext);
                if (user == null)
                {
                    return Unauthorized(new { error = "Incorrect password or email" });
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
