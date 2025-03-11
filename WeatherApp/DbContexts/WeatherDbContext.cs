using Microsoft.EntityFrameworkCore;
using WeatherApp.Models;

namespace WeatherApp.DbContexts
{ 
    public class WeatherDbContext: DbContext
    {
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<WeatherData> WeatherResults { get; set; }
        public DbSet<WeatherHistory> WeatherHistory { get; set; }
    }
}
