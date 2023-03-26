using AutoMapper;
using Mapster;
using Weather.Entities.Models;

namespace Weather.Business.Models;

public class WeatherLocationsBridge
{
    //public List<UiLocation> WeatherLocations { get; set; }

    private WeatherLocationsContext weatherLocationsContext;


    public WeatherLocationsBridge(WeatherLocationsContext weatherLocationsContext)
    {
        this.weatherLocationsContext = weatherLocationsContext;



        List<UiLocation> WeatherLocations = new List<UiLocation>();
        try
        {
            // Call database to retrieve the location list
            WeatherLocations = new List<UiLocation>();
            var c = weatherLocationsContext.Cities.Where(x => x.Name.Contains("Liv"));
            foreach (var location in c)
            {
                var newLocation = location.Adapt<UiLocation>();
                //newLocation.Lat = location.Coord.Lat;
                //newLocation.Long = location.Coord.Long;

                newLocation.Lat = weatherLocationsContext.Coordinates.Where(x => x.coord_id == location.coord_id).FirstOrDefault().Lat;
                newLocation.Long = weatherLocationsContext.Coordinates.Where(x => x.coord_id == location.coord_id).FirstOrDefault().Long;


                WeatherLocations.Add(newLocation);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public List<UiLocation> GetLocationList(string seatchLocation)
    {
        List<UiLocation> WeatherLocations = new List<UiLocation>();
        try
        {
            // Call database to retrieve the location list
            WeatherLocations = new List<UiLocation>();
            var c = weatherLocationsContext.Cities.Where(x => x.Name.Contains(seatchLocation));
            foreach (var location in c)
            {
                var newLocation = location.Adapt<UiLocation>();
                //newLocation.Lat = location.Coord.Lat;
                //newLocation.Long = location.Coord.Long;

                newLocation.Lat = weatherLocationsContext.Coordinates.Where(x => x.coord_id == location.coord_id).FirstOrDefault().Lat;
                newLocation.Long = weatherLocationsContext.Coordinates.Where(x => x.coord_id == location.coord_id).FirstOrDefault().Long;


                WeatherLocations.Add(newLocation);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return WeatherLocations;
    }
}