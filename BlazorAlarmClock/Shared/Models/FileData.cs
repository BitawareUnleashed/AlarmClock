using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAlarmClock.Shared.Models;

public class FileData
{
    public byte[] ImageBytes { get; set; }
    public string FileName { get; set; }
    public string FileType { get; set; }
    public long FileSize { get; set; }
    public string Path { get; set; }
}