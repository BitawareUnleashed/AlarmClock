using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAlarmClock.Shared.Models;

public class FileData
{
    public byte[] DataBytes { get; set; }
    public string FileName { get; set; }
    public string Path { get; set; }
    public bool IsLast { get; set; }
    public Guid FileId { get; set; }
}