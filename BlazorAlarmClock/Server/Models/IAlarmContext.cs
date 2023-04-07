using BlazorAlarmClock.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorAlarmClock.Server.Models;

public interface IAlarmContext
{
    DbSet<Alarm> Alarms { get; set; }
}