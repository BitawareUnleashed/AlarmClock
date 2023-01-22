using BlazorAlarmClock.Server.Models;
using BlazorAlarmClock.Shared.Models;
using Microsoft.Extensions.Options;

namespace BlazorAlarmClock.Server.Controllers;

public static class WeatherController
{
    private const string WeatherApiKeyEndpoint = "/api/v1/GetApiKey";

    public static IEndpointRouteBuilder AddWeatherApiEndpoints(this IEndpointRouteBuilder app)
    {
        _ = app.MapGet(WeatherApiKeyEndpoint, GetWeatherApiKeyApi);
        return app;
    }


    private static IResult GetWeatherApiKeyApi(HttpContext context, IEndpointRouteBuilder app)
    {
        var logging = app?.ServiceProvider?.GetService<IOptions<OpenWeatherMapKey>>()?.Value;

        if (logging == null) { }

        return Results.Ok(logging);
    }
}

