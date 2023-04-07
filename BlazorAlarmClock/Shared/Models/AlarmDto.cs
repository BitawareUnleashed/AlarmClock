using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAlarmClock.Shared.Models;

public class AlarmDto
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the hour.
    /// </summary>
    /// <value>
    /// The hour.
    /// </value>
    public int Hour { get; set; }
    /// <summary>
    /// Gets or sets the minute.
    /// </summary>
    /// <value>
    /// The minute.
    /// </value>
    public int Minute { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether this alarm is active.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
    /// </value>
    public bool IsActive { get; set; }
    /// <summary>
    /// Gets or sets the name of the ringtone.
    /// </summary>
    /// <value>
    /// The name of the ringtone.
    /// </value>
    public string? RingtoneName { get; set; }
    /// <summary>
    /// Gets or sets the days when the alarm will be active.
    /// </summary>
    /// <value>
    /// The alarm days.
    /// </value>
    public List<AlarmDayDto>? AlarmDays { get; set; }

    /// <summary>
    /// Gets or sets the snooze time.
    /// </summary>
    /// <value>
    /// The snooze time.
    /// </value>
    public int SnoozeDelay { get; set; }
}

