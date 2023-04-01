using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Weather.Entities.Models;

[Table("cities")]
public class City
{
    [Key] public int ID { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public int coord_id { get; set; }
}

public class Coordinate
{
    [Key] public int coord_id { get; set; }
    public double Lat { get; set; }
    public double Long { get; set; }
}
