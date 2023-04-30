using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Entities.Models;
public class Settings
{
    /// <summary>
    /// Gets or sets the identifier settings.
    /// </summary>
    /// <value>
    /// The identifier settings.
    /// </value>
    [Key] public int IDSettings { get; set; }
    /// <summary>
    /// Gets or sets the identifier location.
    /// </summary>
    /// <value>
    /// The identifier location.
    /// </value>
    public int IDLocation { get; set; }
    /// <summary>
    /// Gets or sets the location.
    /// </summary>
    /// <value>
    /// The location.
    /// </value>
    public string Location { get; set; }
    /// <summary>
    /// Gets or sets the longitude.
    /// </summary>
    /// <value>
    /// The long.
    /// </value>
    public string Long { get; set; }
    /// <summary>
    /// Gets or sets the latitude.
    /// </summary>
    /// <value>
    /// The lat.
    /// </value>
    public string Lat { get; set; }
}
