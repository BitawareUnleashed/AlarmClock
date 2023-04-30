namespace Weather.Entities.Models;

public class UiLocation
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    public int ID { get; set; }
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the latitude.
    /// </summary>
    /// <value>
    /// The lat.
    /// </value>
    public double Lat { get; set; }
    /// <summary>
    /// Gets or sets the longitude.
    /// </summary>
    /// <value>
    /// The long.
    /// </value>
    public double Long { get; set; }
}