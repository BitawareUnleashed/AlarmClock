using BlazorAlarmClock.Client.Services;
using BlazorAlarmClock.Shared.Models;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace BlazorAlarmClock.Client;

public partial class MainLayout
{
    private MudTheme theme = new();
    private bool isDarkMode = true;

    private AlarmDto newAlarm= new();

    private string ringtoneName = "Select a ringtone";

    bool isOpen;
    private void CancelPopover()
    {
        newAlarm = newAlarm = new();
        isOpen = false;
    }

    private void AddAlarmClosePopover()
    {
        newAlarm.RingtoneName = ringtoneName;
        alarmService.AddNewAlarm(newAlarm);
        isOpen = false;
        StateHasChanged();
    }

    private string? visibility => vis ? "sidebar-hide" : "sidebar";
    bool vis = true;
    private void ToggleNavMenu()
    {
        vis = !vis;
    }

    protected override Task OnInitializedAsync()
    {
        _ = alarmService.GetAlarmList();
        _ = alarmService.GetRingroneList();
        return base.OnInitializedAsync();
    }

    private void ToggleNavPopover()
    {
        ringtoneName = "Select a ringtone";
        newAlarm = new();
        isOpen = !isOpen;
    }

    private async void UploadFiles(IBrowserFile file)
    {
        alarmService.UploadFiles(file);
    }

    private void RingtoneSelected(string ringtoneName)
    {
        this.ringtoneName=ringtoneName.Trim();
    }
}
