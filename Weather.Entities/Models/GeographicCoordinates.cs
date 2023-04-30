namespace Weather.Entities.Models;

public class GeographicCoordinates
{
    /// <summary>
    /// Gets the latitude.
    /// </summary>
    /// <value>
    /// The latitude.
    /// </value>
    public double Latitude { get; private set; } = 0;
    /// <summary>
    /// Gets the longitude.
    /// </summary>
    /// <value>
    /// The longitude.
    /// </value>
    public double Longitude { get; private set; } = 0;

    /// <summary>
    /// Gets the meteo pack from open weather map.
    /// </summary>
    /// <value>
    /// The meteo pack open weather map.
    /// </value>
    public MeteoPack MeteoPackOpenWeatherMap { get; private set; } = new MeteoPack();
    /// <summary>
    /// Gets or sets the icon URL.
    /// </summary>
    /// <value>
    /// The icon URL.
    /// </value>
    public string IconUrl { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="GeographicCoordinates"/> class.
    /// </summary>
    /// <param name="latitude">The latitude.</param>
    /// <param name="longitude">The longitude.</param>
    public GeographicCoordinates(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    /// <summary>
    /// Sets the WWM.
    /// </summary>
    /// <param name="o">The o.</param>
    public void SetWwm(MeteoPack o)
    {
        MeteoPackOpenWeatherMap = o;
    }
}
