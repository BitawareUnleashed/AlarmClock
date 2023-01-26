using Microsoft.EntityFrameworkCore;
using Weather.Entities.Models;

namespace Weather.Business.Data;
public class WeatherDbContext : DbContext
{
    public WeatherDbContext(DbContextOptions<WeatherDbContext> opt)
        : base(opt) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlite(@$"Data Source = Data/owm_cities.sqlite");
    }

}

