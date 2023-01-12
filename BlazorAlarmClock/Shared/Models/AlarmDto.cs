using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAlarmClock.Shared.Models;

public class AlarmDto
{
    public int Id { get; set; }
    public int Hour { get; set; }
    public int Minute { get; set; }
    public bool IsActive { get; set; }
    public string? RingtoneName { get; set; }
    public List<AlarmDayDto>? AlarmDays { get; set; }
}

public class AlarmDayDto
{
    public int AlarmDayId { get; set; }
    public int AlarmId { get; set; }
    public int DayAsInt { get; set; }
}