using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAlarmClock.Shared.Models;

public enum AlarmStatus
{
    NONE = 0x00,
    PLAYING = 0x01,
    SNOOZED = 0x02,
    STOPPED = 0x03,
    STOPPED_TODAY = 0x04
}
