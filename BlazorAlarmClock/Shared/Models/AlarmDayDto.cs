using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAlarmClock.Shared.Models;

public class AlarmDayDto
{
    /// <summary>
    /// Gets or sets the alarm day identifier.
    /// </summary>
    /// <value>
    /// The alarm day identifier.
    /// </value>
    public int AlarmDayId { get; set; }
    /// <summary>
    /// Gets or sets the alarm identifier.
    /// </summary>
    /// <value>
    /// The alarm identifier.
    /// </value>
    public int AlarmId { get; set; }
    /// <summary>
    /// Gets or sets the day as int.
    /// </summary>
    /// <value>
    /// The day as int.
    /// </value>
    public int DayAsInt { get; set; }
}
