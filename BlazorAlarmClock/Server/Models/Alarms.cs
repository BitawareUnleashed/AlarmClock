using BlazorAlarmClock.Shared.Models;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorAlarmClock.Server.Models;

public class Alarms
{
    /// <summary>
    /// The alarm database context
    /// </summary>
    private readonly AlarmDbContext alarmDbContext;
    /// <summary>
    /// Gets or sets the alarms database.
    /// </summary>
    /// <value>
    /// The alarms database.
    /// </value>
    public List<Alarm> AlarmsDb { get; set; }
    /// <summary>
    /// Initializes a new instance of the <see cref="Alarms"/> class.
    /// </summary>
    /// <param name="alarmDbContext">The alarm database context.</param>
    public Alarms(AlarmDbContext alarmDbContext)
    {
        this.alarmDbContext = alarmDbContext;
    }


}
