using BlazorAlarmClock.Server.Models;

namespace BlazorAlarmClock.Server.Controllers;

public static class AlarmController
{
    private const string AlarmListEndpoint = "/api/v1/GetAlarmList";

    public static IEndpointRouteBuilder AddAlarmsApiEndpoints(this IEndpointRouteBuilder app)
    {
        _ = app.MapGet(AlarmListEndpoint, GetAlarmListApi);
        return app;
    }

    private static Task GetAlarmListApi(HttpContext context)
    {
        var a = new Alarms();
       
        return Task.CompletedTask;
    }
}
