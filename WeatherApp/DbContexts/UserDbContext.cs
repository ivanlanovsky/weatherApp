using Microsoft.EntityFrameworkCore;
using WeatherApp.Models;

namespace WeatherApp.DbContexts
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
