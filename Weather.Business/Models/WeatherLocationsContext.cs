using Microsoft.EntityFrameworkCore;
using Weather.Entities.Models;

namespace Weather.Business.Models;

public class WeatherLocationsContext : DbContext
{
    public DbSet<City>? Cities { get; set; }
    //public DbSet<Coordinate>? Coordinates { get; set; }

    public WeatherLocationsContext(DbContextOptions<WeatherLocationsContext> opt)
        : base(opt)
    {
        Database.EnsureCreated();

    }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<City>()
        //     .HasMany(a => a.)
        //     .WithOne(ad => ad.Alarm)
        //     .HasForeignKey(ad => ad.AlarmId);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlite(@$"Data Source = owm_cities.sqlite");
    }

    
}