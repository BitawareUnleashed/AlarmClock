﻿using Microsoft.AspNetCore.Components;
using Weather.Entities.Models;
using Weather.Services;

namespace Weather;

public partial class WeatherComponent
{
    #region Fields

    private int secondsToWait = 900; //15 minutes
    private int currentSecond = 0;
    private int height;

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the open weather service.
    /// </summary>
    /// <value>
    /// The open weather service.
    /// </value>
    [Inject]
    public OpenWeatherService? OpenWeatherService { get; set; }

    /// <summary>
    /// Gets or sets the latitude.
    /// </summary>
    /// <value>
    /// The latitude.
    /// </value>
    [Parameter]
    public double Latitude { get; set; }

    /// <summary>
    /// Gets or sets the longitude.
    /// </summary>
    /// <value>
    /// The longitude.
    /// </value>
    [Parameter]
    public double Longitude { get; set; }

    /// <summary>
    /// Gets or sets the API key.
    /// </summary>
    /// <value>
    /// The API key.
    /// </value>
    public string ApiKey { get; set; }

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

    #region Methods

    /// <inheritdoc />
    protected override async Task OnInitializedAsync()
    {
        var locationTask = openWeatherService.GetSavedLocation();
        await Task.WhenAll(locationTask);
        var location = locationTask.Result;
        Latitude = location.Lat == 0 ? 45.284 : location.Lat;
        Longitude = location.Long == 0 ? 8.077 : location.Long;
        await GetMeteoInformation();

        var dimension = await JsRuntime.InvokeAsync<WindowDimension>("getWindowDimensions", null);
        height = dimension.Height;
        SystemWatch.SecondChangedEvent += SystemWatch_SecondChangedEvent;
    }

    /// <summary>
    /// Systems the watch second changed event.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The DateTime.</param>
    private async void SystemWatch_SecondChangedEvent(object? sender, DateTime e)
    {
        if (++currentSecond >= secondsToWait)
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
        if (OpenWeatherService.OpenWeatherMapApiKey is null)
        {
            await OpenWeatherService.GetApiKey();
        }

        // Get meteo informations from webservice
        MeteoPack = await OpenWeatherService!.Update(Latitude, Longitude, OpenWeatherService.OpenWeatherMapApiKey);

        // Get Icon
        WeatherIcon = OpenWeatherService.GetIcon(MeteoPack);

        Console.WriteLine("Meteo request: " + DateTime.Now);
        StateHasChanged();
    }

    /// <summary>
    /// Gets the temperature rounded.
    /// </summary>
    /// <returns></returns>
    private double GetTemperatureRounded()
    {
        if (MeteoPack is null)
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
    #endregion
}

public class WindowDimension
{
    /// <summary>
    /// Gets or sets the width.
    /// </summary>
    /// <value>
    /// The width.
    /// </value>
    public int Width { get; set; }
    /// <summary>
    /// Gets or sets the height.
    /// </summary>
    /// <value>
    /// The height.
    /// </value>
    public int Height { get; set; }
}