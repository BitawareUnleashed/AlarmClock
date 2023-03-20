namespace BlazorAlarmClock.Client.Components;

public partial class WeatherLocation
{
    private bool resetValueOnEmptyText;
    private bool coerceValue;
    private bool coerceText;
    private string value1;
    
    
    public List<string> Locations { get; set; } = new();
    
    protected override async Task OnInitializedAsync()
    {
        Locations = await openWeatherService.GetLocationsList();
        StateHasChanged();
    }
    
    private async Task<IEnumerable<string>> Search1(string value)
    {
        // In real life use an asynchronous function for fetching data from an api.
        await Task.Delay(5);

        // if text is null or empty, show complete list
        if (string.IsNullOrEmpty(value))
            return Locations;
        var ret=Locations.Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase)).ToList();
        return ret;
    }
    
    
    private void OnClick(string location)
    {
        
    }
}