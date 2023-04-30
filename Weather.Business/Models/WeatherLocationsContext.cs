using Microsoft.EntityFrameworkCore;
using Weather.Entities.Models;

namespace Weather.Business.Models;

public class WeatherLocationsContext : DbContext
{
    /// <summary>
    /// Gets or sets the cities.
    /// </summary>
    /// <value>
    /// The cities.
    /// </value>
    public DbSet<City>? Cities { get; set; }
    /// <summary>
    /// Gets or sets the coordinates.
    /// </summary>
    /// <value>
    /// The coordinates.
    /// </value>
    public DbSet<Coordinate>? Coordinates { get; set; }
    /// <summary>
    /// Gets or sets the conditions.
    /// </summary>
    /// <value>
    /// The conditions.
    /// </value>
    public DbSet<Condition>? Conditions { get; set; }
    /// <summary>
    /// Gets or sets the settings.
    /// </summary>
    /// <value>
    /// The settings.
    /// </value>
    public DbSet<Settings>? Settings { get; set; }
    /// <summary>
    /// Initializes a new instance of the <see cref="WeatherLocationsContext"/> class.
    /// </summary>
    /// <param name="opt">The opt.</param>
    public WeatherLocationsContext(DbContextOptions<WeatherLocationsContext> opt)
        : base(opt)
    {

        if (!File.Exists(@"../../Weather.Business/Data/owm_cities.sqlite"))
        {
            InitializeDatabase();
        }
        Database.EnsureCreated();
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<City>()
            .HasOne(c => c.Coord)
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
    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@$"Data Source =  ../../Weather.Business/Data/owm_cities.sqlite");
    }

    /// <summary>
    /// Saves the settings.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="location">The location.</param>
    /// <param name="lat">The latitude.</param>
    /// <param name="lon">The longitude.</param>
    public void SaveSettings(int id, string location, double lat, double lon)
    {
        if (Settings.Any())
        {
            Settings.FirstOrDefault().IDLocation = id;
            Settings.FirstOrDefault().Location = location;
            Settings.FirstOrDefault().Lat = lat.ToString();
            Settings.FirstOrDefault().Long = lon.ToString();
        }
        else
        {
            Settings.Add(new Settings()
            {
                IDLocation = id,
                Location = location,
                Lat = lat.ToString(),
                Long = lon.ToString()
            });
        }
        SaveChanges();
        Settings.LoadAsync();
    }



}