using BlazorAlarmClock.Server.Models;
using BlazorAlarmClock.Shared.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BlazorAlarmClock.Server.Controllers;

public static class AlarmController
{
    private const string AlarmListEndpoint = "/api/v1/GetAlarmList";

    //protected readonly IRepository<EntityType, IdType> _repository;


    public static IEndpointRouteBuilder AddAlarmsApiEndpoints(this IEndpointRouteBuilder app)
    {
        _ = app.MapGet(AlarmListEndpoint, GetAlarmListApi);
        return app;
    }

    private static Task GetAlarmListApi(HttpContext context, Alarms alarms, IRepository<Alarm, int> repo)
    {
        var a = repo.GetAll();
        return Task.CompletedTask;
    }
}
