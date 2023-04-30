using MudBlazor;
using Weather.Entities.Models;

namespace Weather.Components;

public partial class WeatherLocation
{
    /// <summary>
    /// The loading
    /// </summary>
    private bool loading;

    /// <summary>
    /// The insert location
    /// </summary>
    private string insertLocation = string.Empty;

    /// <summary>
    /// Gets or sets the locations.
    /// </summary>
    /// <value>
    /// The locations.
    /// </value>
    public List<UiLocation> Locations { get; set; } = new();

    /// <summary>
    /// Searches the location.
    /// </summary>
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

    /// <summary>
    /// Rows the click event.
    /// </summary>
    /// <param name="tableRowClickEventArgs">The <see cref="TableRowClickEventArgs{UiLocation}"/> instance containing the event data.</param>
    private async void RowClickEvent(TableRowClickEventArgs<UiLocation> tableRowClickEventArgs)
    {
        // TODO: Save location
        var a = tableRowClickEventArgs.Item;
        Console.WriteLine(tableRowClickEventArgs.Item.Name);
        Console.WriteLine(tableRowClickEventArgs.Item.Lat);
        Console.WriteLine(tableRowClickEventArgs.Item.Long);

        openWeatherService.SaveLocation(tableRowClickEventArgs.Item);
        navigationManager.NavigateTo("/", true);
    }
}