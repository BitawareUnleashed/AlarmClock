using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Services;

namespace Weather;

public static class Program
{
    public static IServiceCollection AddWeather(this IServiceCollection services)
    {
        
        return services;
    }
}
