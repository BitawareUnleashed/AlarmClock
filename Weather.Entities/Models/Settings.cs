using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Entities.Models;
public class Settings
{
    [Key]
    public int IDSettings { get; set; }
    public int IDLocation { get; set; }
	public string Location { get; set; }
    public string Long { get; set; }
	public string Lat { get; set; }
}
