using Mapster;
using Microsoft.EntityFrameworkCore;
using Weather.Entities.Models;

namespace Weather.Business.Models;

public class WeatherLocationsBridge
{
    /// <summary>
    /// The weather locations context
    /// </summary>
    private WeatherLocationsContext weatherLocationsContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="WeatherLocationsBridge"/> class.
    /// </summary>
    /// <param name="weatherLocationsContext">The weather locations context.</param>
    public WeatherLocationsBridge(WeatherLocationsContext weatherLocationsContext)
    {
        this.weatherLocationsContext = weatherLocationsContext;
    }

    /// <summary>
    /// Gets the location list.
    /// </summary>
    /// <param name="seatchLocation">The seatch location.</param>
    /// <returns></returns>
    public List<UiLocation> GetLocationList(string seatchLocation)
    {
        List<UiLocation> WeatherLocations = new List<UiLocation>();
        try
        {
            // Call database to retrieve the location list
            WeatherLocations = new List<UiLocation>();
            var c = weatherLocationsContext.Cities
                .Include(x => x.Coord)
                .Where(x => x.Name.Contains(seatchLocation))
                .Take(20);


            foreach (var location in c)
            {
                var newLocation = location.Adapt<UiLocation>();
                newLocation.Lat = location.Coord.Lat;
                newLocation.Long = location.Coord.Long;
                newLocation.ID = location.Id;
                WeatherLocations.Add(newLocation);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return WeatherLocations;
    }

    /// <summary>
    /// Saves the location.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="lat">The lat.</param>
    /// <param name="lon">The lon.</param>
    /// <param name="id">The identifier.</param>
    public void SaveLocation(string name, double lat, double lon, int id)
    {
        weatherLocationsContext.SaveSettings(id, name, lat, lon);
    }

    /// <summary>
    /// Gets the saved location.
    /// </summary>
    /// <returns></returns>
    public string GetSavedLocation() => weatherLocationsContext.Settings.FirstOrDefault().IDLocation.ToString();

    /// <summary>
    /// Gets the location from identifier.
    /// </summary>
    /// <param name="locationId">The location identifier.</param>
    /// <returns></returns>
    public (string name, double lat, double lon) GetLocationFromId(string locationId)
    {
        var locationDbId = int.Parse(locationId);
        if (weatherLocationsContext.Cities != null)
        {
            var location = weatherLocationsContext.Cities
                .Include(x => x.Coord).FirstOrDefault(x => x.Id == locationDbId);
            if (location is null)
            {
                return ("", 0, 0);
            }
            return (location.Name, location.Coord.Lat, location.Coord.Long);
        }

        return ("", 0, 0);
    }
}