using Microsoft.EntityFrameworkCore;
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
        //modelBuilder.Entity<City>()
        //    .HasOne(a => a.Coord)
        //    .WithMany();


        //modelBuilder
        //    .Entity<City>()
        //    .HasOne<Coordinate>()
        //    .WithOne(i => i.City)
        //    .HasForeignKey<Coordinate>(e => e.Id);

        modelBuilder
            .Entity<City>()
            .HasOne(c=>c.Coord)
            .WithOne(co => co.City)
            .HasForeignKey<Coordinate>(co => co.coord_id);

        modelBuilder.Entity<Coordinate>()
            .HasOne(c => c.City)
            .WithOne(co => co.Coord)
            .HasForeignKey<City>(c => c.coord_id);


        /*CONDITIONS FEED*/

        //modelBuilder.Entity<Condition>().HasData(new Condition() { Cond_id = 64, Id = 804, Main = "Clouds", Description = "overcast clouds: 85-100%", Icon = "04d" });


        /*COORDINATES FEED
        modelBuilder.Entity<Coordinate>().HasData(new Coordinate() { coord_id = 2960, Lat = 34.940079, Long = 36.321911 });
        modelBuilder.Entity<Coordinate>().HasData(new Coordinate() { coord_id = 3174655, Lat = 45.284061, Long = 8.07743 });
        modelBuilder.Entity<City>().HasData(new City() { Id = 3174655, Name = "Livorno Ferraris", Country = "IT", coord_id = 3174655 });
        */
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