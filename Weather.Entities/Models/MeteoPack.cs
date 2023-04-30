namespace Weather.Entities.Models;

public class MeteoPack
{
    /// <summary>
    /// Gets or sets the coord.
    /// </summary>
    /// <value>
    /// The coord.
    /// </value>
    public Coord coord { get; set; } = new Coord();
    /// <summary>
    /// Gets or sets the weather.
    /// </summary>
    /// <value>
    /// The weather.
    /// </value>
    public Weather[] weather { get; set; }
    /// <summary>
    /// Gets or sets the base.
    /// </summary>
    /// <value>
    /// The base.
    /// </value>
    public string _base { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the main.
    /// </summary>
    /// <value>
    /// The main.
    /// </value>
    public Main main { get; set; } = new Main();
    /// <summary>
    /// Gets or sets the visibility.
    /// </summary>
    /// <value>
    /// The visibility.
    /// </value>
    public int visibility { get; set; } = 0;
    /// <summary>
    /// Gets or sets the wind.
    /// </summary>
    /// <value>
    /// The wind.
    /// </value>
    public Wind wind { get; set; } = new Wind();
    /// <summary>
    /// Gets or sets the clouds.
    /// </summary>
    /// <value>
    /// The clouds.
    /// </value>
    public Clouds clouds { get; set; } = new Clouds();
    /// <summary>
    /// Gets or sets the dt.
    /// </summary>
    /// <value>
    /// The dt.
    /// </value>
    public int dt { get; set; } = 0;
    /// <summary>
    /// Gets or sets the system.
    /// </summary>
    /// <value>
    /// The system.
    /// </value>
    public Sys sys { get; set; } = new Sys();
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    public int id { get; set; } = 0;
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    public string name { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the cod.
    /// </summary>
    /// <value>
    /// The cod.
    /// </value>
    public int cod { get; set; } = 0;
    /// <summary>
    /// Gets or sets the error message.
    /// </summary>
    /// <value>
    /// The error message.
    /// </value>
    public string ErrorMessage { get; set; } = string.Empty;


}

public class Coord
{
    /// <summary>
    /// Gets or sets the longitude.
    /// </summary>
    /// <value>
    /// The lon.
    /// </value>
    public float lon { get; set; }
    /// <summary>
    /// Gets or sets the latitude.
    /// </summary>
    /// <value>
    /// The lat.
    /// </value>
    public float lat { get; set; }
}

public class Main
{
    /// <summary>
    /// Gets or sets the temporary.
    /// </summary>
    /// <value>
    /// The temporary.
    /// </value>
    public float temp { get; set; }
    /// <summary>
    /// Gets or sets the pressure.
    /// </summary>
    /// <value>
    /// The pressure.
    /// </value>
    public int pressure { get; set; }
    /// <summary>
    /// Gets or sets the humidity.
    /// </summary>
    /// <value>
    /// The humidity.
    /// </value>
    public int humidity { get; set; }
    /// <summary>
    /// Gets or sets the minimum temperature.
    /// </summary>
    /// <value>
    /// The temporary minimum.
    /// </value>
    public float temp_min { get; set; }
    /// <summary>
    /// Gets or sets the maximum temperature.
    /// </summary>
    /// <value>
    /// The temporary maximum.
    /// </value>
    public float temp_max { get; set; }
}

public class Wind
{
    /// <summary>
    /// Gets or sets the speed.
    /// </summary>
    /// <value>
    /// The speed.
    /// </value>
    public float speed { get; set; }
    /// <summary>
    /// Gets or sets the degrees.
    /// </summary>
    /// <value>
    /// The deg.
    /// </value>
    public int deg { get; set; }
}

public class Clouds
{
    /// <summary>
    /// Gets or sets all.
    /// </summary>
    /// <value>
    /// All.
    /// </value>
    public int all { get; set; }
}

public class Sys
{
    /// <summary>
    /// Gets or sets the type.
    /// </summary>
    /// <value>
    /// The type.
    /// </value>
    public int type { get; set; }
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    public int id { get; set; }
    /// <summary>
    /// Gets or sets the message.
    /// </summary>
    /// <value>
    /// The message.
    /// </value>
    public float message { get; set; }
    /// <summary>
    /// Gets or sets the country.
    /// </summary>
    /// <value>
    /// The country.
    /// </value>
    public string country { get; set; }
    /// <summary>
    /// Gets or sets the sunrise.
    /// </summary>
    /// <value>
    /// The sunrise.
    /// </value>
    public int sunrise { get; set; }
    /// <summary>
    /// Gets or sets the sunset.
    /// </summary>
    /// <value>
    /// The sunset.
    /// </value>
    public int sunset { get; set; }

    /// <summary>
    /// Gets the sunrise date time as string.
    /// </summary>
    /// <returns></returns>
    public string GetSunriseDateTimeString()
    {
        var ret = string.Empty;
        var date = (new DateTime(1970, 1, 1)).AddMilliseconds((double)sunrise * 1000).ToLocalTime();
        ret = date.ToLongTimeString();
        return ret;
    }

    /// <summary>
    /// Gets the sunset date time as string.
    /// </summary>
    /// <returns></returns>
    public string GetSunsetDateTimeString()
    {
        var ret = string.Empty;
        var date = (new DateTime(1970, 1, 1)).AddMilliseconds((double)sunset * 1000).ToLocalTime();
        ret = date.ToLongTimeString();
        return ret;
    }

    /// <summary>
    /// Gets the sunrise date time.
    /// </summary>
    /// <returns></returns>
    public DateTime GetSunriseDateTime()
    {
        var ret = DateTime.Now;
        ret = (new DateTime(1970, 1, 1)).AddMilliseconds((double)sunrise * 1000).ToLocalTime();
        return ret;
    }

    /// <summary>
    /// Gets the sunset date time.
    /// </summary>
    /// <returns></returns>
    public DateTime GetSunsetDateTime()
    {
        var ret = DateTime.Now;
        ret = (new DateTime(1970, 1, 1)).AddMilliseconds((double)sunset * 1000).ToLocalTime();
        return ret;
    }
}

public class Weather
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    public int id { get; set; }
    /// <summary>
    /// Gets or sets the main.
    /// </summary>
    /// <value>
    /// The main.
    /// </value>
    public string main { get; set; }
    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>
    /// The description.
    /// </value>
    public string description { get; set; }
    /// <summary>
    /// Gets or sets the icon.
    /// </summary>
    /// <value>
    /// The icon.
    /// </value>
    public string icon { get; set; }
}
