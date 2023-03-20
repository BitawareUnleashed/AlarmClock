namespace Weather.Business.Models;

public class WeatherLocationsBridge
{
    public List<string> WeatherLocations { get; set; }
    
    public WeatherLocationsBridge(WeatherLocationsContext weatherLocationsContext)
    {
        // Call database to retrieve the location list
        WeatherLocations = new List<string>();
        var c = weatherLocationsContext.Cities;
        foreach (var location in c)
        {
            WeatherLocations.Add(location.Name);    
        }
    }
}