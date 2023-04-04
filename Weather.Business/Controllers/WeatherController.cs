using Microsoft.AspNetCore.Routing;
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
    private const string WeatherLocationsEndpointFilter = "/api/v1/GetWeatherLocations/{location}";
    private const string WeatherSingleLocationsEndpoint = "/api/v1/GetSingle/{location}";
    private const string WeatherSaveLocationEndpoint = "/api/v2/PostSaveLocation/{name}/{lat}/{lon}/{id}";
    //private const string WeatherSaveLocationEndpoint = "/api/v1/PostSaveLocation";
    


    private static string? ApiKey = string.Empty;
    private static List<string> WeatherLocations = new List<string>();

    public static IEndpointRouteBuilder AddWeatherApiEndpoints(this IEndpointRouteBuilder app)
    {
        ApiKey = app?.ServiceProvider?.GetService<IOptions<OpenWeatherMapKey>>()?.Value?.ApiKey;

        _ = app.MapGet(WeatherApiKeyEndpoint, GetWeatherApiKeyApi);
        _ = app.MapGet(WeatherLocationsEndpoint, GetLocationList);
        _ = app.MapGet(WeatherSingleLocationsEndpoint, GetLocationFilterList);
        _ = app.MapGet(WeatherSaveLocationEndpoint, PostSaveLocationList);
        return app;
    }


    private static IResult GetWeatherApiKeyApi()
    {
        return Results.Ok(ApiKey);
    }

    public static IResult GetLocationList([FromServices] WeatherLocationsBridge weatherLocationsBridge)
    {
        //var a = weatherLocationsBridge.WeatherLocations;
        return Results.Ok();
    }
    
    public static IResult GetLocationFilterList(string location, [FromServices] WeatherLocationsBridge weatherLocationsBridge)
    {
        var a = weatherLocationsBridge.GetLocationList(location);
        return Results.Ok(a);
    }

    
    public static IResult PostSaveLocationList([FromServices] WeatherLocationsBridge weatherLocationsBridge, string name, string lat, string lon, string id)
    {
        var itemName = name;
        var latitude = SharedMethods.Base64Decode(lat);
        var longitude = SharedMethods.Base64Decode(lon);
        var identifier = id;

        weatherLocationsBridge.SaveLocation(name, lat, lon, id);


        return Results.Ok();
    }
}

