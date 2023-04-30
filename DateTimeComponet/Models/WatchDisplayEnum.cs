namespace DateTimeComponent.Models;

public enum WatchDisplayEnum
{
    /// <summary>
    /// None
    /// </summary>
    None = 0x00,
    /// <summary>
    /// With seconds
    /// </summary>
    WithSeconds = 0x01,
    /// <summary>
    /// With blinking
    /// </summary>
    WithBlinking = 0x02,
    /// <summary>
    /// With seconds and blinking
    /// </summary>
    WithSecondsAndBlinking = 0x03,
    /// <summary>
    /// Undefined value
    /// </summary>
    Undefined = 0xFE,
    /// <summary>
    /// Unknown value
    /// </summary>
    Unknown = 0xFF
}
