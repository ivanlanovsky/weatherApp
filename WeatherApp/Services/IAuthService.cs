using Microsoft.IdentityModel.Tokens;
using WeatherApp.DTO;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public interface IAuthService
    {
        Task<User?> Authenticate(UserCredentials credentials, HttpContext context);
    }
}
