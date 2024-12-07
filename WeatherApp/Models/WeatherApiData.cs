namespace WeatherApp.Models
{
    public class WeatherApiData
    {
        public Main Main { get; set; }
        public Weather[] Weather { get; set; }
        public string Name { get; set; }
    }
}
