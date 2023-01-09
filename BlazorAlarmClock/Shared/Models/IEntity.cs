namespace BlazorAlarmClock.Server.Models;
public interface IEntity<TKey>
{
    TKey Id { get; set; }
}

