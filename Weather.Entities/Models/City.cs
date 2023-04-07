using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Weather.Entities.Models;

[Table("cities")]
public class City
{
    [Key] public int Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public int coord_id { get; set; }

    public virtual Coordinate Coord { get; set; }
}

public class Coordinate
{
    [Key] public int coord_id { get; set; }
    public double Lat { get; set; }
    public double Long { get; set; }

    public City City { get; set; }
}

public class Condition
{
    [Key] public int Cond_id { get; set; }
    public int Id { get; set; }
    public string Main { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
}
