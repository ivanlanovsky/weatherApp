using System.Runtime.CompilerServices;
using WeatherApp.Models;

namespace WeatherApp.Extentions
{
    public static class MappingExtentions
    {
        public static WeatherData ToWeatherDto(this WeatherApiData weatherData)
        {
            return new WeatherData
            {
                City = weatherData.Name,
                Temperature = weatherData.Main.Temp,
                Humidity = weatherData.Main.Humidity,
                Description = weatherData.Weather[0].Description,
            };
        } 

    }
}
