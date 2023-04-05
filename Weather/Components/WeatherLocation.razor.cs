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

    private async void RowClickEvent(TableRowClickEventArgs<UiLocation> tableRowClickEventArgs)
    {
        // TODO: Save location
        var a = tableRowClickEventArgs.Item;
        Console.WriteLine(tableRowClickEventArgs.Item.Name);
        Console.WriteLine(tableRowClickEventArgs.Item.Lat);
        Console.WriteLine(tableRowClickEventArgs.Item.Long);

        openWeatherService.SaveLocation(tableRowClickEventArgs.Item.Name,tableRowClickEventArgs.Item.Lat,tableRowClickEventArgs.Item.Long,tableRowClickEventArgs.Item.ID);

        await Task.Delay(3000);
        var b= openWeatherService.GetSavedLocation();
        if (b is not null)
        {
            
        }
    }
}