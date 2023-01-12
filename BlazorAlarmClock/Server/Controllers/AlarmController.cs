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

    private static IResult GetAlarmListApi(HttpContext context, AlarmDataRepository repo)
    {
        
        var a = repo.GetAll();
        var dto = new List<AlarmDto>();
        foreach (var item in a.ToList())
        {
            var almDays = new List<AlarmDayDto>();
            if(item.AlarmDays is not null)
            {
                foreach (var day in item.AlarmDays)
                {
                    almDays.Add(new AlarmDayDto()
                    {
                        AlarmDayId=day.AlarmDayId,
                        AlarmId=day.AlarmId,
                        DayAsInt=day.DayAsInt
                    });
                }
            }

            dto.Add(new AlarmDto()
            {
                Hour=item.Hour,
                Id=item.Id,
                IsActive=item.IsActive,
                Minute=item.Minute,
                RingtoneName=item.RingtoneName,
                AlarmDays= almDays
            });
        }


        return Results.Ok(dto);
    }

    private static IResult PostNewAlarmApi([FromBody]Alarm alarm, HttpContext context, AlarmDataRepository repo)
    {
        repo.CreateAsync(alarm);
        return Results.Ok();
    }


}
