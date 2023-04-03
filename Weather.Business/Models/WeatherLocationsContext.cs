﻿using Microsoft.EntityFrameworkCore;
using Weather.Entities.Models;

namespace Weather.Business.Models;

public class WeatherLocationsContext : DbContext
{
    public DbSet<City>? Cities { get; set; }
    public DbSet<Coordinate>? Coordinates { get; set; }
    public DbSet<Condition>? Conditions { get; set; }

    public WeatherLocationsContext(DbContextOptions<WeatherLocationsContext> opt)
        : base(opt)
    {
        if (!File.Exists(@"Data\owm_cities.sqlite"))
        {
            InitializeDatabase();
        }
        Database.EnsureCreated();
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<City>()
            .HasOne(c=>c.Coord)
            .WithOne(co => co.City)
            .HasForeignKey<Coordinate>(co => co.coord_id);

        modelBuilder.Entity<Coordinate>()
            .HasOne(c => c.City)
            .WithOne(co => co.Coord)
            .HasForeignKey<City>(c => c.coord_id);
    }

    /// <summary>
    /// Initializes the database from SQL file.
    /// </summary>
    public void InitializeDatabase()
    {
#if (DEBUG)
        Database.ExecuteSqlRaw(File.ReadAllText(@"../../Weather.Business/Data/owm_cities.sql"));
#else
        Database.ExecuteSqlRaw(File.ReadAllText(@"Data\owm_cities.sql"));
#endif

    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@$"Data Source = Data\owm_cities.sqlite");
    }
}