using BlazorAlarmClock.Shared.Models;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorAlarmClock.Server.Models;

public class Alarms
{
    private readonly AlarmDbContext alarmDbContext;

    public List<Alarm> AlarmsDb { get; set; }
    public Alarms(AlarmDbContext alarmDbContext)
    {
        this.alarmDbContext = alarmDbContext;
    }


}
