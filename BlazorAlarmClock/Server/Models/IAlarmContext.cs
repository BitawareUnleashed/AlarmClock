using BlazorAlarmClock.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorAlarmClock.Server.Models;

public interface IAlarmContext
{
    /// <summary>
    /// Gets or sets the alarms.
    /// </summary>
    /// <value>
    /// The alarms.
    /// </value>
    DbSet<Alarm> Alarms { get; set; }
}