using BlazorAlarmClock.Server.Models;
using BlazorAlarmClock.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BlazorAlarmClock.Server.Controllers;

public static class AlarmController
{
    private const string AlarmListEndpoint = "/api/v1/GetAlarmList";
    private const string AddNewAlarmEndpoint = "/api/v1/AddNewAlarm";
    private const string DeleteAlarmEndpoint = "/api/v1/DeleteAlarm";
    private const string UpdateAlarmEndpoint = "/api/v1/UpdateAlarm";

    public static IEndpointRouteBuilder AddAlarmsApiEndpoints(this IEndpointRouteBuilder app)
    {
        _ = app.MapGet(AlarmListEndpoint, GetAlarmListApi);
        _ = app.MapPost(AddNewAlarmEndpoint, PostNewAlarmApi);
        _ = app.MapPost(DeleteAlarmEndpoint, PostDeleteAlarmApi);
        _ = app.MapPost(UpdateAlarmEndpoint, PostUpdateAlarmApi);
        return app;
    }

    private static IResult GetAlarmListApi(HttpContext context, AlarmDataRepository repo)
    {

        var a = repo.GetAll();
        var dto = new List<AlarmDto>();
        foreach (var item in a.ToList())
        {
            var almDays = new List<AlarmDayDto>();
            if (item.AlarmDays is not null)
            {
                foreach (var day in item.AlarmDays)
                {
                    almDays.Add(new AlarmDayDto()
                    {
                        AlarmDayId = day.AlarmDayId,
                        AlarmId = day.AlarmId,
                        DayAsInt = day.DayAsInt
                    });
                }
            }

            dto.Add(new AlarmDto()
            {
                Hour = item.Hour,
                Id = item.Id,
                IsActive = item.IsActive,
                Minute = item.Minute,
                RingtoneName = item.RingtoneName,
                AlarmDays = almDays
            });
        }


        return Results.Ok(dto);
    }

    private static async Task<IResult> PostDeleteAlarmApi([FromBody] int alarmId, HttpContext context, AlarmDataRepository repo)
    {
        await repo.DeleteAsync(alarmId);
        return Results.Ok();
    }

    private static async Task<IResult> PostNewAlarmApi([FromBody] AlarmDto alarm, HttpContext context, AlarmDataRepository repo)
    {
        Alarm newAlarm = ConvertToAlarm(alarm);

        await repo.CreateAsync(newAlarm);
        return Results.Ok();
    }

    private static async Task<IResult> PostUpdateAlarmApi([FromBody] AlarmDto alarm, HttpContext context, AlarmDataRepository repo)
    {
        Alarm newAlarm = ConvertToAlarm(alarm);

        await repo.UpdateAsync(newAlarm);
        return Results.Ok();
    }

    private static Alarm ConvertToAlarm(AlarmDto alarm)
    {
        var alarmDays = new List<AlarmDay>();
        if (alarm.AlarmDays is not null)
        {
            foreach (var day in alarm.AlarmDays)
            {
                alarmDays.Add(new AlarmDay()
                {
                    AlarmDayId = day.AlarmDayId,
                    AlarmId = day.AlarmId,
                    DayAsInt = day.DayAsInt
                });
            }
        }

        var newAlarm = new Alarm()
        {
            AlarmDays = alarmDays,
            Hour = alarm.Hour,
            Id = alarm.Id,
            IsActive = alarm.IsActive,
            Minute = alarm.Minute,
            RingtoneName = alarm.RingtoneName
        };
        return newAlarm;
    }
}
