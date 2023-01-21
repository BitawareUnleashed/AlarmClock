using BlazorAlarmClock.Client.Components;
using BlazorAlarmClock.Shared.Models;
using MudBlazor;

namespace BlazorAlarmClock.Client;

public partial class MainLayout
{
    private MudTheme theme = new();
    private bool isDarkMode = true;
    bool isOpen;
    
    AlarmDto alarmDto;

    private string? visibility => vis ? "sidebar-hide" : "sidebar";
    bool vis = true;
    private void ToggleNavMenu()
    {
        vis = !vis;
    }

    private void ToggleNavPopover()
    {
        //ringtoneName = "Select a ringtone";
        //newAlarm = new();
        isOpen = !isOpen;
    }

    private void PopoverChanged(bool popoverOpened)
    {
        isOpen = popoverOpened;
    }

    public void OpenRequest(AlarmDto alarm)
    {
        alarmDto= alarm;
        isOpen = true;
        StateHasChanged();
    }
}
