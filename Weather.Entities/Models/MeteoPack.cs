namespace Weather.Entities.Models;

public class MeteoPack
{
    public string AddressRoute { get { return this.id.ToString("000000000"); } }
    public Coord coord { get; set; } = new Coord();
    public Weather[] weather { get; set; }
    public string _base { get; set; } = string.Empty;
    public Main main { get; set; } = new Main();
    public int visibility { get; set; } = 0;
    public Wind wind { get; set; } = new Wind();
    public Clouds clouds { get; set; } = new Clouds();
    public int dt { get; set; } = 0;
    public Sys sys { get; set; } = new Sys();
    public int id { get; set; } = 0;
    public string name { get; set; } = string.Empty;
    public int cod { get; set; } = 0;

    public string ErrorMessage { get; set; } = string.Empty;


}

public class Coord
{
    public float lon { get; set; }
    public float lat { get; set; }
}

public class Main
{
    public float temp { get; set; }
    public int pressure { get; set; }
    public int humidity { get; set; }
    public float temp_min { get; set; }
    public float temp_max { get; set; }
}

public class Wind
{
    public float speed { get; set; }
    public int deg { get; set; }
}

public class Clouds
{
    public int all { get; set; }
}

public class Sys
{
    public int type { get; set; }
    public int id { get; set; }
    public float message { get; set; }
    public string country { get; set; }
    public int sunrise { get; set; }
    public int sunset { get; set; }

    public string GetSunriseDateTimeString()
    {
        var ret = string.Empty;
        var date = (new DateTime(1970, 1, 1)).AddMilliseconds((double)sunrise * 1000).ToLocalTime();
        ret = date.ToLongTimeString();
        return ret;
    }
    public string GetSunsetDateTimeString()
    {
        var ret = string.Empty;
        var date = (new DateTime(1970, 1, 1)).AddMilliseconds((double)sunset * 1000).ToLocalTime();
        ret = date.ToLongTimeString();
        return ret;
    }

    public DateTime GetSunriseDateTime()
    {
        var ret = DateTime.Now;
        ret = (new DateTime(1970, 1, 1)).AddMilliseconds((double)sunrise * 1000).ToLocalTime();
        return ret;
    }
    public DateTime GetSunsetDateTime()
    {
        var ret = DateTime.Now;
        ret = (new DateTime(1970, 1, 1)).AddMilliseconds((double)sunset * 1000).ToLocalTime();
        return ret;
    }
}

public class Weather
{
    public int id { get; set; }
    public string main { get; set; }
    public string description { get; set; }
    public string icon { get; set; }
}
