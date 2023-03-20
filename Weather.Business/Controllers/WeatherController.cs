using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using BlazorAlarmClock.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Weather.Business.Models;

namespace Weather.Business.Controllers;
public static class WeatherController
{
    private const string WeatherApiKeyEndpoint = "/api/v1/GetApiKey";
    private const string WeatherLocationsEndpoint = "/api/v1/GetWeatherLocations";
    private static string? ApiKey = string.Empty;
    private static List<string> WeatherLocations = new List<string>();

    public static IEndpointRouteBuilder AddWeatherApiEndpoints(this IEndpointRouteBuilder app)
    {
        ApiKey = app?.ServiceProvider?.GetService<IOptions<OpenWeatherMapKey>>()?.Value?.ApiKey;
        if (ApiKey == null) { }
        _ = app.MapGet(WeatherApiKeyEndpoint, GetWeatherApiKeyApi);
        _ = app.MapGet(WeatherLocationsEndpoint, GetLocationList);

        return app;
    }


    private static IResult GetWeatherApiKeyApi()
    {
        return Results.Ok(ApiKey);
    }

    public static IResult GetLocationList([FromServices] WeatherLocationsBridge weatherLocationsBridge)
    {
        var a = weatherLocationsBridge.WeatherLocations;
        return Results.Ok(a);
    }
}

