using MudBlazor;
using Weather.Entities.Models;

namespace Weather.Components;

public partial class WeatherLocation
{
    private bool loading;
 
    private string insertLocation = string.Empty;
    public List<UiLocation> Locations { get; set; } = new();
    
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

    private void RowClickEvent(TableRowClickEventArgs<UiLocation> tableRowClickEventArgs)
    {
        // TODO: Save location
        var a = tableRowClickEventArgs.Item;
        Console.WriteLine(tableRowClickEventArgs.Item.Name);
    }
}