using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAlarmClock.Shared.Models;

public class AlarmFluentFactory
{
    Alarm alarm = new Alarm();

    /// <summary>
    /// Hours of the alarm.
    /// </summary>
    /// <param name="hour">The hour.</param>
    /// <returns></returns>
    public AlarmFluentFactory HourAlarm(int hour)
    {
        alarm.Hour = hour;
        return this;
    }

    /// <summary>
    /// Minutes of the alarm.
    /// </summary>
    /// <param name="minute">The minute.</param>
    /// <returns></returns>
    public AlarmFluentFactory MinuteAlarm(int minute)
    {
        alarm.Minute = minute;
        return this;
    }

    /// <summary>
    /// Determines whether the alarm is active.
    /// </summary>
    /// <param name="isActive">if set to <c>true</c> [is active].</param>
    /// <returns></returns>
    public AlarmFluentFactory IsActiveAlarm(bool isActive)
    {
        alarm.IsActive = isActive;
        return this;
    }

    /// <summary>
    /// Ringtone of the alarm.
    /// </summary>
    /// <param name="ringtone">The ringtone.</param>
    /// <returns></returns>
    public AlarmFluentFactory RingtoneAlarm(string ringtone)
    {
        alarm.RingtoneName = ringtone;
        return this;
    }

    /// <summary>
    /// Adds a day on the alarm.
    /// </summary>
    /// <param name="day">The day.</param>
    /// <returns></returns>
    public AlarmFluentFactory AddDayAlarm(int day)
    {
        alarm.AlarmDays.Add(new AlarmDay()
        {
            DayAsInt = day
        });
        return this;
    }

    /// <summary>
    /// Gets the alarm built.
    /// </summary>
    /// <returns></returns>
    public Alarm GetAlarm()
    {
        return alarm;
    }
}

