﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAlarmClock.Shared.Models;

public class FileData
{
    /// <summary>
    /// Gets or sets the data bytes.
    /// </summary>
    /// <value>
    /// The data bytes.
    /// </value>
    public byte[] DataBytes { get; set; }
    /// <summary>
    /// Gets or sets the name of the file.
    /// </summary>
    /// <value>
    /// The name of the file.
    /// </value>
    public string FileName { get; set; }
    /// <summary>
    /// Gets or sets the path.
    /// </summary>
    /// <value>
    /// The path.
    /// </value>
    public string Path { get; set; }
}