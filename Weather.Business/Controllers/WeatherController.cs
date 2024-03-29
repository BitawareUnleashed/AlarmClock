﻿using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using BlazorAlarmClock.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Weather.Business.Models;
using Weather.Entities.Models;

namespace Weather.Business.Controllers;

public static class WeatherController
{
    private const string WeatherApiKeyEndpoint = "/api/v1/GetApiKey";
    private const string WeatherLocationsEndpoint = "/api/v1/GetWeatherLocations";
    private const string WeatherSingleLocationsEndpoint = "/api/v1/GetSingle/{location}";
    private const string WeatherSaveLocationEndpoint = "/api/v2/PostSaveLocation/{name}/{lat}/{lon}/{id}";
    private const string WeatherGetLocationEndpoint = "/api/v1/GetSavedLocation";


    private static string? ApiKey = string.Empty;

    public static IEndpointRouteBuilder AddWeatherApiEndpoints(this IEndpointRouteBuilder app)
    {
        ApiKey = app?.ServiceProvider?.GetService<IOptions<OpenWeatherMapKey>>()?.Value?.ApiKey;

        _ = app.MapGet(WeatherApiKeyEndpoint, GetWeatherApiKeyApi);
        _ = app.MapGet(WeatherLocationsEndpoint, GetLocationList);
        _ = app.MapGet(WeatherSingleLocationsEndpoint, GetLocationFilterList);
        _ = app.MapGet(WeatherSaveLocationEndpoint, GetSaveLocationList);
        _ = app.MapGet(WeatherGetLocationEndpoint, GetSavedLocationList);
        return app;
    }

    /// <summary>
    /// Gets the weather API key API.
    /// </summary>
    /// <returns></returns>
    private static IResult GetWeatherApiKeyApi()
    {
        return Results.Ok(ApiKey);
    }

    /// <summary>
    /// Gets the location list.
    /// </summary>
    /// <param name="weatherLocationsBridge">The weather locations bridge.</param>
    /// <returns></returns>
    public static IResult GetLocationList([FromServices] WeatherLocationsBridge weatherLocationsBridge)
    {
        //var a = weatherLocationsBridge.WeatherLocations;
        return Results.Ok();
    }

    /// <summary>
    /// Gets the location filter list.
    /// </summary>
    /// <param name="location">The location.</param>
    /// <param name="weatherLocationsBridge">The weather locations bridge.</param>
    /// <returns></returns>
    public static IResult GetLocationFilterList(string location,
        [FromServices] WeatherLocationsBridge weatherLocationsBridge)
    {
        var a = weatherLocationsBridge.GetLocationList(location);
        return Results.Ok(a);
    }

    /// <summary>
    /// Gets the save location list.
    /// </summary>
    /// <param name="weatherLocationsBridge">The weather locations bridge.</param>
    /// <param name="name">The name.</param>
    /// <param name="lat">The latitude.</param>
    /// <param name="lon">The longitude.</param>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    public static IResult GetSaveLocationList([FromServices] WeatherLocationsBridge weatherLocationsBridge, string name,
        string lat, string lon, string id)
    {
        var itemName = name;
        var latitude = SharedMethods.Base64Decode(lat);
        var longitude = SharedMethods.Base64Decode(lon);
        var identifier = id;

        weatherLocationsBridge.SaveLocation(name, double.Parse(latitude), double.Parse(longitude), int.Parse(id));

        return Results.Ok();
    }

    /// <summary>
    /// Gets the saved location list.
    /// </summary>
    /// <param name="weatherLocationsBridge">The weather locations bridge.</param>
    /// <returns></returns>
    public static IResult GetSavedLocationList([FromServices] WeatherLocationsBridge weatherLocationsBridge)
    {
        var locationId = weatherLocationsBridge.GetSavedLocation();
        (string name, double lat, double lon) = weatherLocationsBridge.GetLocationFromId(locationId);
        var returnLocation = new UiLocation()
        {
            Lat = lat,
            Long = lon,
            Name = name,
            ID = int.Parse(locationId)
        };
        return Results.Ok(returnLocation);
    }
}