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
    private const string UploadFileRingtoneEndpoint = "/api/v1/UploadRingtone";
    private const string GetRingtoneListEndpoint = "/api/v1/GetRingtonesList";
    private const string DeleteAlarmRingtoneEndpoint = "/api/v1/DeleteAlarmRingtone";

    public static IEndpointRouteBuilder AddAlarmsApiEndpoints(this IEndpointRouteBuilder app)
    {
        _ = app.MapGet(AlarmListEndpoint, GetAlarmListApi);
        _ = app.MapGet(GetRingtoneListEndpoint, GetRingtoneListApi);

        _ = app.MapPost(AddNewAlarmEndpoint, PostNewAlarmApi);
        _ = app.MapPost(DeleteAlarmEndpoint, PostDeleteAlarmApi);
        _ = app.MapPost(UpdateAlarmEndpoint, PostUpdateAlarmApi);
        _ = app.MapPost(UploadFileRingtoneEndpoint, SaveRingtone);

        _ = app.MapPost(DeleteAlarmRingtoneEndpoint, PostDeleteRingtoneApi);
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
                AlarmDays = almDays,
                SnoozeDelay=item.SnoozeTime
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
        if (newAlarm.Id > 0)
        {
            await repo.UpdateAsync(newAlarm);
        }
        else
        {
            await repo.CreateAsync(newAlarm);
        }
        return Results.Ok();
    }

    private static async Task<IResult> PostUpdateAlarmApi([FromBody] AlarmDto alarm, HttpContext context, AlarmDataRepository repo)
    {
        Alarm newAlarm = ConvertToAlarm(alarm);

        await repo.UpdateAsync(newAlarm);
        return Results.Ok();
    }

    
    private static async Task<IResult> PostDeleteRingtoneApi([FromBody] string alarmRingtoneName, HttpContext context, AlarmDataRepository repo)
    {
        string dir = string.Empty;
        var path = "wwwroot/audio";

#if (DEBUG)
        dir = Directory.GetCurrentDirectory() + "\\bin\\Debug\\net7.0\\";
#else
        dir = Directory.GetCurrentDirectory();
#endif
        var filePath = Path.Combine(dir, path, alarmRingtoneName);

        File.Delete(filePath);

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
            RingtoneName = alarm.RingtoneName,
            SnoozeTime=alarm.SnoozeDelay
        };
        return newAlarm;
    }

    private static async Task<IResult> SaveRingtone([FromBody] FileData file)
    {
        try
        {
            string dir = string.Empty;
#if(DEBUG)
            dir = Directory.GetCurrentDirectory() + "\\bin\\Debug\\net7.0\\";
#else
            dir = Directory.GetCurrentDirectory();
#endif
            var filePath = Path.Combine(dir, file.Path, file.FileName);

            using (var fileStream = System.IO.File.Create(filePath))
            {
                await fileStream.WriteAsync(file.DataBytes);
            }
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.StatusCode(500);
        }
    }

    private static async Task<IResult> GetRingtoneListApi()
    {
        string dir = string.Empty;
        var path = "wwwroot/audio";

#if (DEBUG)
        dir = Directory.GetCurrentDirectory() + "\\bin\\Debug\\net7.0\\";
#else
        dir = Directory.GetCurrentDirectory();
#endif
        var filePath = Path.Combine(dir, path);

        var list = Directory.GetFiles(filePath);
        var returnList = new List<string>();
        foreach (var file in list)
        {
            returnList.Add(file.Substring(file.LastIndexOf("\\") + 1));
        }

        return Results.Ok(returnList.ToList());
    }
}

