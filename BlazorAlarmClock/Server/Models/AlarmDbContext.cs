using BlazorAlarmClock.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorAlarmClock.Server.Models;

public class AlarmDbContext : DbContext
{
    public DbSet<Alarm> Alarms { get; set; }

    public AlarmDbContext(DbContextOptions opt)
        : base(opt) { }

    //protected override void OnConfiguring(DbContextOptionsBuilder options) 
    //    => options.UseSqlite(@$"Data Source = Alarms.sqlite");

    public void Save()
    {
        this.SaveChanges();
    }
}
