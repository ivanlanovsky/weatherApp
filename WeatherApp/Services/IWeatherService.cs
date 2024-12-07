using WeatherApp.Models;

namespace WeatherApp.Services
{
    public interface IWeatherService
    {
        Task<WeatherData> GetWeatherDataAsync(string city);
    }
}
