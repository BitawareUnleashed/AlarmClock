using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Models;
using Weather.Services;

namespace Weather;

public partial class WeatherComponent
{
    private int secondsToWait = 3600; // one hour
    private int currentSecond = 0;

    private int height;

    #region Properties    
    /// <summary>
    /// Gets or sets the open weather service.
    /// </summary>
    /// <value>
    /// The open weather service.
    /// </value>
    [Inject] public OpenWeatherService? OpenWeatherService { get; set; }

    /// <summary>
    /// Gets or sets the latitude.
    /// </summary>
    /// <value>
    /// The latitude.
    /// </value>
    [Parameter] public double Latitude { get; set; }

    /// <summary>
    /// Gets or sets the longitude.
    /// </summary>
    /// <value>
    /// The longitude.
    /// </value>
    [Parameter] public double Longitude { get; set; }

    /// <summary>
    /// Gets or sets the API key.
    /// </summary>
    /// <value>
    /// The API key.
    /// </value>
    [Parameter] public string ApiKey { get; set; }

    /// <summary>
    /// Gets the meteo pack.
    /// </summary>
    /// <value>
    /// The meteo pack.
    /// </value>
    public MeteoPack? MeteoPack { get; private set; }
    /// <summary>
    /// Gets or sets the weather icon.
    /// </summary>
    /// <value>
    /// The weather icon.
    /// </value>
    public string WeatherIcon { get; set; }


    #endregion

    protected override async Task OnInitializedAsync()
    {
        var dimension = await JsRuntime.InvokeAsync<WindowDimension>("getWindowDimensions", null);
        height = dimension.Height;
        SystemWatch.SecondChangedEvent += SystemWatch_SecondChangedEvent;
        await GetMeteoInformation();
    }

    /// <summary>
    /// Systems the watch second changed event.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The DateTime.</param>
    private async void SystemWatch_SecondChangedEvent(object? sender, DateTime e)
    {
        if (++currentSecond == secondsToWait)
        {
            currentSecond = 0;
            await GetMeteoInformation();
        }
    }


    /// <summary>
    /// Gets the meteo informations.
    /// </summary>
    private async Task GetMeteoInformation()
    {
        MeteoPack = await OpenWeatherService!.Update(Latitude, Longitude, ApiKey); ;

        // Get Icon
        WeatherIcon = OpenWeatherService.GetIcon(MeteoPack);

        Console.WriteLine("Meteo request: " + DateTime.Now);
        StateHasChanged();
    }



    private double GetTemperatureRounded()
    {
        if (MeteoPack is not null)
        {
            return 0;
        }

        return Math.Round(MeteoPack.main.temp, 1);
    }


    /// <summary>
    /// Gets the height of the image as 50% of the height of the screen.
    /// </summary>
    /// <returns></returns>
    private int GetImageHeight() => height / 2;

    private string GetImageWidthStyle() => $"width: {GetImageHeight()}";

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources asynchronously.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous dispose operation.
    /// </returns>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore().ConfigureAwait(false);

        Dispose(disposing: false);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (SystemWatch is not null)
            {
                SystemWatch.SecondChangedEvent -= SystemWatch_SecondChangedEvent;
            }
            Console.WriteLine("Dispose");
        }
    }

    /// <summary>
    /// Disposes asynchronous.
    /// </summary>
    /// <returns></returns>
    protected virtual async ValueTask DisposeAsyncCore()
    {
        if (SystemWatch is not null)
        {
            SystemWatch.SecondChangedEvent -= SystemWatch_SecondChangedEvent;
        }

        Console.WriteLine("DisposeAsync");
    }
}
public class WindowDimension
{
    public int Width { get; set; }
    public int Height { get; set; }
}