using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WeatherApp.DTO;
using WeatherApp.Exceptions;
using WeatherApp.Services;
using WeatherApp.Models;
using Microsoft.AspNetCore.Cors;
using WeatherApp.DbContexts;
using Microsoft.AspNetCore.Authorization;
using WeatherApp.Common;


namespace WeatherApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;
        private readonly WeatherDbContext _dbContext;

        public AuthController(ILogger<AuthController> logger, IAuthService authService, WeatherDbContext dbContext)
        {
            _logger = logger;
            _authService = authService;
            _dbContext = dbContext;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> LoginWithCookie(UserCredentials credentials)
        {
            try
            {
                var user = await _authService.LoginWithCookie(credentials, HttpContext);
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

        [HttpPost("[action]")]
        public async Task<IActionResult> LoginWithJwt(UserCredentials credentials)
        {
            try
            {
                var token = await _authService.GetAuthJwtToken(credentials, HttpContext);
                if (token == string.Empty)
                {
                    return Unauthorized(new { error = "Incorrect password or email" });
                }
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Logout(User user)
        {
            await _authService.Logout(HttpContext);
            return Ok();
        }

        [Authorize(Policy = StringConstants.AdminPolicyTitle)]
        [HttpPost("[action]")]
        public async Task<IActionResult> GetNumberOfUsers()
        {
            var count = await _dbContext.Users.CountAsync();
            return Ok(count);
        }
    }
}
