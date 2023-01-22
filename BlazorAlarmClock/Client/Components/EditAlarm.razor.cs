using BlazorAlarmClock.Client.Services;
using BlazorAlarmClock.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAlarmClock.Client.Components;

public partial class EditAlarm
{
    private const string? ringtoneFileNameEmpty = "Select a ringtone";
    private const string? minutesMeasureUnits = "Minutes";

    /// <summary>
    /// Gets or sets the name of the ringtone file.
    /// </summary>
    /// <value>
    /// The name of the ringtone file.
    /// </value>
    public string? RingtoneFileName { get; set; }


    [Parameter] public string? Title { get; set; } = "Create a new alarm.";
    /// <summary>
    /// Gets or sets the ringtone file name when is empty.
    /// </summary>
    /// <value>
    /// The empty ringtone file name.
    /// </value>
    [Parameter] public string? RingtoneFileNameEmpty { get; set; }

    /// <summary>
    /// Gets or sets the is popover open changed callback to the parent.
    /// </summary>
    /// <value>
    /// The is popover open changed.
    /// </value>
    [Parameter] public EventCallback<bool> IsPopoverOpenChanged { get; set; }

    /// <summary>
    /// Gets or sets the current in edit or new alarm.
    /// </summary>
    /// <value>
    /// The alarm.
    /// </value>
    [Parameter] public AlarmDto? Alarm { get; set; }

    /// <summary>
    /// Cancels the popover.
    /// </summary>
    public void CancelPopover()
    {
        RingtoneFileName = null;
        IsPopoverOpenChanged.InvokeAsync(false);
        Alarm = new();
    }

    /// <summary>
    /// Closes the and save the current alarm.
    /// </summary>
    public void CloseAndSavePopover()
    {
        Alarm ??= new()
        {
            SnoozeTime = 5
        };

        if (!string.IsNullOrEmpty(RingtoneFileName))
        {
            Alarm.RingtoneName = RingtoneFileName;
        }
        else
        {
            Alarm.RingtoneName = null;
        }
        alarmService.AddOrUpdateAlarm(Alarm);
        IsPopoverOpenChanged.InvokeAsync(false);
        StateHasChanged();
    }

    protected override Task OnInitializedAsync()
    {
        Alarm ??= new()
        {
            SnoozeTime = 5
        };

        alarmService.OnRingtoneUploaded += AlarmService_OnRingtoneUploaded;
        alarmService.OnErrorRaised += AlarmService_OnErrorRaised;
        _ = alarmService.GetAlarmList();
        _ = alarmService.GetRingroneList();
        return base.OnInitializedAsync();
    }

    /// <summary>
    /// From Alarms service, on ringtone file uploaded snackbar.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    public void AlarmService_OnRingtoneUploaded(object? sender, string e)
    {
        Snackbar.Add
        (
            @$"<div>
                <h3>Ringtone</h3>
                <ul>
                    <li>Ringtone file '{e}' uploaded</li>
                </ul>
            </div>",
            Severity.Success
        );
    }

    /// <summary>
    /// From Alarms service, when an error raised.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    private void AlarmService_OnErrorRaised(object? sender, string e)
    {
        Snackbar.Add
        (
            @$"<div>
                <h3>Error</h3>
                <ul>
                    <li>{e}</li>
                </ul>
            </div>",
            Severity.Error
        );
    }

    /// <summary>
    /// Uploads the files for ringtones.
    /// </summary>
    /// <param name="file">The file.</param>
    public void UploadFiles(IBrowserFile file) => alarmService.UploadFiles(file);

    /// <summary>
    /// Ringtones selected.
    /// </summary>
    /// <param name="ringtoneName">Name of the ringtone.</param>
    public void RingtoneSelected(string ringtoneName) => RingtoneFileName = ringtoneName.Trim();

    public string GetFilename() => RingtoneFileName ?? ringtoneFileNameEmpty;

}
