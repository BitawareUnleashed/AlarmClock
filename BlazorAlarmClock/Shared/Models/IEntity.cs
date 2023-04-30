namespace BlazorAlarmClock.Server.Models;
public interface IEntity<TKey>
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    TKey Id { get; set; }
}

