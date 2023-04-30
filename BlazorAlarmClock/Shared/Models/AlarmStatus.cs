using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAlarmClock.Shared.Models;

public enum AlarmStatus
{
    /// <summary>
    /// No value
    /// </summary>
    NONE = 0x00,
    /// <summary>
    /// The playing status
    /// </summary>
    PLAYING = 0x01,
    /// <summary>
    /// The snoozed status
    /// </summary>
    SNOOZED = 0x02,
    /// <summary>
    /// The stopped status
    /// </summary>
    STOPPED = 0x03,
    /// <summary>
    /// The alarm was stopped today
    /// </summary>
    STOPPED_TODAY = 0x04,
    /// <summary>
    /// The alarm status is undefined
    /// </summary>
    UNDEFINED = 0xFE,
    /// <summary>
    /// The alarm status is unknown
    /// </summary>
    UNKNOWN = 0xFF
}
