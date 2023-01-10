using BlazorAlarmClock.Server.Models;
using BlazorAlarmClock.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BlazorAlarmClock.Server.Controllers;

public static class AlarmController
{
    private const string AlarmListEndpoint = "/api/v1/GetAlarmList";
    private const string AddNewAlarmEndpoint = "/api/v1/AddNewAlarm";
    
    public static IEndpointRouteBuilder AddAlarmsApiEndpoints(this IEndpointRouteBuilder app)
    {
        _ = app.MapGet(AlarmListEndpoint, GetAlarmListApi);
        _ = app.MapPost(AddNewAlarmEndpoint, PostNewAlarmApi);
        return app;
    }

    private static IResult GetAlarmListApi(HttpContext context, IRepository<Alarm, int> repo)
    {
        var a = repo.GetAll();
        return Results.Ok(a);
    }

    private static IResult PostNewAlarmApi([FromBody]Alarm alarm, HttpContext context, IRepository<Alarm, int> repo)
    {
        repo.CreateAsync(alarm);
        return Results.Ok();
    }


}
