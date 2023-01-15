using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAlarmClock.Client.Localizer;

public interface ITextLocalizer
{
    /// <summary>
    /// Localizes the specified text.
    /// </summary>
    /// <param name="textId">The text to localize.</param>
    /// <returns></returns>
    string Localize(string textId);
}
