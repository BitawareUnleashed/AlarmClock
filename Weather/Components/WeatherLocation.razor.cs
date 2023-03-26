using Weather.Entities.Models;

namespace Weather.Components;

public partial class WeatherLocation
{
    private bool resetValueOnEmptyText;
    private bool coerceValue;
    private bool coerceText;
    private bool _loading;

    private string insertLocation = string.Empty;
    public List<UiLocation> Locations { get; set; } = new();
    
    // protected override async Task OnInitializedAsync()
    // {
    //     Locations = await openWeatherService.GetLocationsList();
    //     StateHasChanged();
    // }
    
    // private async Task<IEnumerable<string>> SearchLocation(string value)
    // {
    //     // In real life use an asynchronous function for fetching data from an api.
    //     await Task.Delay(5);
    //
    //     // if text is null or empty, show complete list
    //     if (string.IsNullOrEmpty(value))
    //         return Locations;
    //     var ret=Locations.Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase)).ToList();
    //     return ret;
    // }
    
    
    private async void SearchLocation()
    {
        try
        {
            Locations = await openWeatherService.GetLocationsList(insertLocation);
            StateHasChanged();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void LocationClick(string chosenLocation)
    {
        
    }
}