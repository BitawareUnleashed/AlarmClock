using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Weather.Business.Controllers;
using Weather.Business.Models;

namespace Weather.Business;

public static class Program
{
    public static IServiceCollection AddWeatherBusiness(this IServiceCollection services)
    {
        services.AddDbContext<WeatherLocationsContext>(opt =>
            opt.UseSqlite(@$"Data Source = Data\owm_cities.sqlite"));
        services.AddScoped<DbContext, WeatherLocationsContext>();
        services.AddScoped<WeatherLocationsBridge>();
        return services;
    }

    public static WebApplication UseWeather(this WebApplication app)
    {
        app.AddWeatherApiEndpoints();
        return app;
    }
}