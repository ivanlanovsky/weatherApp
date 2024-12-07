namespace WeatherApp.Models
{
    public class WeatherData
    {   
        public int Id { get; set; }
        public string City { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public string Description { get; set; }
    }
}
