using BlazorAlarmClock.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorAlarmClock.Server.Models;

public class AlarmContext:DbContext
{
    public DbSet<Alarm> Alarms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) 
        => options.UseSqlite(@$"Data Source = Alarms.sqlite");

    public void Save()
    {
        this.SaveChanges();
    }



}
