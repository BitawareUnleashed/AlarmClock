using AutoMapper;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Weather.Entities.Models;

namespace Weather.Business.Models;

public class WeatherLocationsBridge
{
    private WeatherLocationsContext weatherLocationsContext;

    public WeatherLocationsBridge(WeatherLocationsContext weatherLocationsContext)
    {
        this.weatherLocationsContext = weatherLocationsContext;
    }

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

    public void SaveLocation(string name, string lat, string lon, string id) => weatherLocationsContext.SaveSettings(int.Parse(id), name, lat, lon);
    
    public int GetSavedLocation() => weatherLocationsContext.Settings.FirstOrDefault().IDLocation;
}