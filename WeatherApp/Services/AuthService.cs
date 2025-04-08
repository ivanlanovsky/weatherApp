using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WeatherApp.DbContexts;
using WeatherApp.DTO;
using WeatherApp.Models;
using WeatherApp.Common;
using Azure;

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

        public async Task<User?> LoginWithCookie(UserCredentials credentials, HttpContext context)
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

        public async Task<User> LoginWithJwt(UserCredentials credentials, HttpContext context)
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
                var secret = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("SecretKey"));
                var key = new SymmetricSecurityKey(secret);
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    "issuer",
                    "audience",
                    claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds);
                context.Response.Cookies.Append("jwt", new JwtSecurityTokenHandler().WriteToken(token), new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddHours(1)
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
