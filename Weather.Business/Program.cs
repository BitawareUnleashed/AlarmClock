﻿using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Weather.Business.Data;

namespace Weather.Business;

    public static class Program
    {
        public static IServiceCollection AddWeatherBusiness(this IServiceCollection services)
        {
            services.AddDbContext<WeatherDbContext>(opt =>
                opt.UseSqlite(@$"Data Source = Data/owm_cities.sqlite"));
        return services;
        }
}

