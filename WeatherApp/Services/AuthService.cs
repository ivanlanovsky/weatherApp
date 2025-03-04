using Microsoft.EntityFrameworkCore;
using WeatherApp.DbContexts;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    internal class AuthService : IAuthService
    {
        private readonly UserDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public AuthService(UserDbContext userDbContext, IConfiguration configuration) {
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
