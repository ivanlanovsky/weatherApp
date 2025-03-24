using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using WeatherApp.DbContexts;
using WeatherApp.DTO;
using WeatherApp.Models;
using WeatherApp.Common;

namespace WeatherApp.Services
{
    internal class AuthService : IAuthService
    {
        private readonly WeatherDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public AuthService(WeatherDbContext userDbContext, IConfiguration configuration)
        {
            _dbContext = userDbContext;
            _configuration = configuration;
        }

        public async Task<User?> Login(UserCredentials credentials, HttpContext context)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(
                (u) => u.Email == credentials.Email && u.Password == credentials.Password);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                };

                var identity = new ClaimsIdentity(claims, StringConstants.WeatherAppAuth);
                var claimsPrincipal = new ClaimsPrincipal(identity);
                await context.SignInAsync(StringConstants.WeatherAppAuth, claimsPrincipal, 
                    new AuthenticationProperties() 
                    { 
                        IsPersistent = true 
                    });
            }

            return user;
        }

        public async Task Logout(HttpContext context)
        {
            await context.SignOutAsync();
        }
    }
}
