using Microsoft.IdentityModel.Tokens;
using WeatherApp.DTO;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public interface IAuthService
    {
        Task<User?> LoginWithCookie(UserCredentials credentials, HttpContext context);

        Task<string> GetAuthJwtToken(UserCredentials credentials, HttpContext context);

        Task Logout(HttpContext context);

    }
}
