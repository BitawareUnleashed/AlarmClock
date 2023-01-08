using BlazorAlarmClock.Shared.Models;

namespace BlazorAlarmClock.Server.Models;

public class Alarms
{
    public List<Alarm> AlarmsDb { get; set; }

    public Alarms()
	{
        using (var db = new AlarmContext())
        {
            AlarmsDb = db.Alarms.ToList();

        }
    }
}
