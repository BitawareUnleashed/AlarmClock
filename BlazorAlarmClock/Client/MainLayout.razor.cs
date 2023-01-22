using BlazorAlarmClock.Client.Components;
using BlazorAlarmClock.Shared.Models;
using MudBlazor;

namespace BlazorAlarmClock.Client;

public partial class MainLayout
{
    private string createNewAlarmText= "Create a new alarm";
    private string editAlarmText= "Edit alarm with id:";
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
        isOpen = !isOpen;
        // Open as new Alarm
        if (isOpen)
        {
            alarmDto = new();
            EditAlarmTitle = createNewAlarmText;
        }
    }

    private void PopoverChanged(bool popoverOpened)
    {
        isOpen = popoverOpened;
    }

    public void OpenRequest(AlarmDto alarm)
    {
        EditAlarmTitle = $"{editAlarmText} {alarm.Id}";
        alarmDto = alarm;
        isOpen = true;
        StateHasChanged();
    }

    private string EditAlarmTitle = string.Empty;
}
