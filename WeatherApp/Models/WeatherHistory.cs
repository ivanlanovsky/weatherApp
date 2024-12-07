namespace WeatherApp.Models
{
    public class WeatherHistory
    {
        public int Id { get; set; }
        public string? City { get; set; }
        public DateTime Date { get; set; }

        public int WeatherDataId {  get; set; }
        public WeatherData? WeatherData { get; set; }
    }
}
