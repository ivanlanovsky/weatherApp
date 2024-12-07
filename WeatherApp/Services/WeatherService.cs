using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WeatherApp.DbContexts;
using WeatherApp.Exceptions;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class WeatherService: IWeatherService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly WeatherDbContext _dbContext;

        private readonly IDictionary<string, WeatherData> _weatherDataCache;

        public WeatherService(HttpClient httpClient, IConfiguration configuration,
            WeatherDbContext dbContext)
        {
            _client = httpClient;
            _configuration = configuration;
            _dbContext = dbContext;
            _weatherDataCache = new Dictionary<string, WeatherData>();
        }

        public async Task<WeatherData> GetWeatherDataAsync(string city)
        {
            if (_weatherDataCache.TryGetValue(city, out WeatherData data))
            {
                return data;
            }

            string baseUrl = _configuration["ApiBaseUrl"];
            string apiKey = _configuration["ApiKey"];
            string url = $"{baseUrl}weather?q={city}&appid={apiKey}&units=metric";

            HttpResponseMessage response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var weatherData = JsonConvert.DeserializeObject<WeatherApiData>(responseBody);

                var weatherResult = new WeatherData
                {
                    City = city,
                    Temperature = weatherData.Main.Temp,
                    Humidity = weatherData.Main.Humidity,
                    Description = weatherData.Weather[0].Description,
                };
                _dbContext.WeatherResults.Add(weatherResult);
                await _dbContext.SaveChangesAsync();

                var weatherHistory = new WeatherHistory
                {
                    City = city,
                    Date = DateTime.UtcNow,
                    WeatherDataId = weatherResult.Id
                };
                _dbContext.WeatherHistory.Add(weatherHistory);
                await _dbContext.SaveChangesAsync();

                _weatherDataCache.Add(city, weatherResult);
                return weatherResult;
            }
            else
            {
                throw new ApiErrorException(response);
            }
        }
    }
}
