using BlazorAlarmClock.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorAlarmClock.Server.Extensions;

public static class ProgramExtensions
{
    public static async Task<IApplicationBuilder> InizializeDatabases(this WebApplication app)
    {
        using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope())
        {
            var serviceProvider = serviceScope.ServiceProvider;
            IConfiguration config = serviceProvider.GetRequiredService<IConfiguration>();
            var alarmDbContext = serviceProvider.GetRequiredService<AlarmDbContext>();

            //Add last migrations
            alarmDbContext.Database.Migrate();

            await EnsureInitialSeeding(config, alarmDbContext);

        }
        return app;
    }

    private static Task EnsureInitialSeeding(IConfiguration config, AlarmDbContext dbContext)
    {
        if (!dbContext.Alarms.Any())
        {
            //dbContext.SaveChanges();
        }
        return Task.CompletedTask;
    }

}

