using BlazorAlarmClock.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorAlarmClock.Server.Models;

public class AlarmDbContext : DbContext
{
    public DbSet<Alarm> Alarms { get; set; }

    public AlarmDbContext(DbContextOptions<AlarmDbContext> opt)
        : base(opt) { }

protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alarm>()
                    .HasMany(a => a.AlarmDays)
                    .WithOne(ad => ad.Alarm)
                    .HasForeignKey(ad => ad.AlarmId);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlite(@$"Data Source = Alarms.sqlite");
    }

    public void Save()
    {
        this.SaveChanges();
    }
}
