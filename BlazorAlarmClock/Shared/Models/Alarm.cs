using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using BlazorAlarmClock.Server.Models;

namespace BlazorAlarmClock.Shared.Models;

public class Alarm:IEntity<int>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int Hour { get; set; }
    public int Minute { get; set; }
    public bool IsActive { get; set; }
    public string? RingtoneName { get; set; }
    public int SnoozeTime { get; set; }
    public virtual ICollection<AlarmDay> AlarmDays { get; set; }
}

public class AlarmDay
{
    public int AlarmDayId { get; set; }
    public int AlarmId { get; set; }
    public int DayAsInt { get; set; }
    public virtual Alarm Alarm { get; set; }
}