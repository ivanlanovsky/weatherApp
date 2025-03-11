using Microsoft.EntityFrameworkCore;
using WeatherApp.DbContexts;
using WeatherApp.DTO;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    internal class AuthService : IAuthService
    {
        private readonly WeatherDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public AuthService(WeatherDbContext userDbContext, IConfiguration configuration) {
            _dbContext = userDbContext;
            _configuration = configuration;
        }

        public async Task<User?> Authenticate(UserCredentials credentials)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(
                (u) => u.Email == credentials.Email && u.Password == credentials.Password);
        }
    }
}
