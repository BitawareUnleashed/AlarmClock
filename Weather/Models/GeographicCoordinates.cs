using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Models;

public class GeographicCoordinates
{
    public double Latitude { get; private set; } = 0;
    public double Longitude { get; private set; } = 0;

    public MeteoPack MeteoPackOpenWeatherMap { get; private set; } = new MeteoPack();
    public string IconUrl { get; set; } = string.Empty;

    public GeographicCoordinates(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public void SetWwm(MeteoPack o)
    {
        MeteoPackOpenWeatherMap = o;
    }
}
