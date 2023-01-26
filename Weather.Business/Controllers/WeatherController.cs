using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using BlazorAlarmClock.Server.Models;

namespace Weather.Business.Controllers;
public static class WeatherController
{
    private const string WeatherApiKeyEndpoint = "/api/v1/GetApiKey";
    private static string? ApiKey = string.Empty;

    public static IEndpointRouteBuilder AddWeatherApiEndpoints(this IEndpointRouteBuilder app)
    {
        ApiKey = app?.ServiceProvider?.GetService<IOptions<OpenWeatherMapKey>>()?.Value?.ApiKey;
        if (ApiKey == null) { }
        _ = app.MapGet(WeatherApiKeyEndpoint, GetWeatherApiKeyApi);
        return app;
    }


    private static IResult GetWeatherApiKeyApi()
    {
        //var logging = app?.ServiceProvider?.GetService<IOptions<OpenWeatherMapKey>>()?.Value;
        //if (logging == null) { }

        return Results.Ok(ApiKey);
    }
}

