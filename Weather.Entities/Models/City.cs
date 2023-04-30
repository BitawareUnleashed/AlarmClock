using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Weather.Entities.Models;

[Table("cities")]
public class City
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    [Key] public int Id { get; set; }
    /// <summary>
    /// Gets or sets the name of the city.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    public string Name { get; set; }
    /// <summary>
    /// Gets or sets the country.
    /// </summary>
    /// <value>
    /// The country.
    /// </value>
    public string Country { get; set; }
    /// <summary>
    /// Gets or sets the coord identifier.
    /// </summary>
    /// <value>
    /// The coord identifier.
    /// </value>
    public int coord_id { get; set; }

    /// <summary>
    /// Gets or sets the coordinate.
    /// </summary>
    /// <value>
    /// The coord.
    /// </value>
    public virtual Coordinate Coord { get; set; }
}

public class Coordinate
{
    /// <summary>
    /// Gets or sets the coord identifier.
    /// </summary>
    /// <value>
    /// The coord identifier.
    /// </value>
    [Key] public int coord_id { get; set; }
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
    /// <summary>
    /// Gets or sets the city.
    /// </summary>
    /// <value>
    /// The city.
    /// </value>
    public City City { get; set; }
}

public class Condition
{
    /// <summary>
    /// Gets or sets the cond identifier.
    /// </summary>
    /// <value>
    /// The cond identifier.
    /// </value>
    [Key] public int Cond_id { get; set; }
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the main.
    /// </summary>
    /// <value>
    /// The main.
    /// </value>
    public string Main { get; set; }
    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>
    /// The description.
    /// </value>
    public string Description { get; set; }
    /// <summary>
    /// Gets or sets the icon.
    /// </summary>
    /// <value>
    /// The icon.
    /// </value>
    public string Icon { get; set; }
}
