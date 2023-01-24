namespace Weather.Models;

public class City
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public Coordinate Coord { get; set; }
}

public class Coordinate
{
    //[Key]
    public int coord_id { get; set; }
    public double Lat { get; set; }
    public double Long { get; set; }
}


public class Condition
{
    //[Key]
    public int Cond_id { get; set; }
    public int ID { get; set; }
    public string Main { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
}
