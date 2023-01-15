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

public partial class NewAlarm
{
    private const string ringtoneFileNameEmpty = "Select a ringtone";
    private const string minutesMeasureUnits = "Minutes";
    private AlarmDto newAlarm = new();

    public string RingtoneFileName { get; set; }
    [Parameter] public string RingtoneFileNameEmpty { get; set; }

    [Parameter] public EventCallback<bool>  IsPopoverOpenChanged { get; set; }

    private void CancelPopover()
    {
        newAlarm = new()
        {
            SnoozeTime = 5
        };
        RingtoneFileName = null;
        IsPopoverOpenChanged.InvokeAsync(false);
    }

    private void AddAlarmClosePopover()
    {
        if (!string.IsNullOrEmpty(RingtoneFileName))
        {
            newAlarm.RingtoneName = RingtoneFileName;
        }
        else
        {
            newAlarm.RingtoneName = null;
        }
        alarmService.AddNewAlarm(newAlarm);
        IsPopoverOpenChanged.InvokeAsync(false);
        StateHasChanged();
    }

    protected override Task OnInitializedAsync()
    {
        newAlarm = new()
        {
            SnoozeTime = 5
        };
        alarmService.OnRingtoneUploaded += AlarmService_OnRingtoneUploaded;
        alarmService.OnErrorRaised += AlarmService_OnErrorRaised;
        _ = alarmService.GetAlarmList();
        _ = alarmService.GetRingroneList();
        return base.OnInitializedAsync();
    }



    private void AlarmService_OnRingtoneUploaded(object? sender, string e)
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

    private async void UploadFiles(IBrowserFile file)
    {
        alarmService.UploadFiles(file);
    }

    private void RingtoneSelected(string ringtoneName)
    {
        this.RingtoneFileName = ringtoneName.Trim();
    }
}
