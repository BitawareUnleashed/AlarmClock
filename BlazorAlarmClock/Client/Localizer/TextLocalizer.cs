using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAlarmClock.Client.Localizer;

public class TextLocalizer : ITextLocalizer
{
    private readonly IStringLocalizer? localizer;

    public TextLocalizer(IStringLocalizerFactory factory, Type language) => localizer = factory.Create(language);


    /// <inheritdoc cref="ITextLocalizer" />
    /// >
    public string Localize(string textId)
    {
        if (localizer is null)
        {
            return textId;
        }

        return localizer.GetString(textId);
    }
}