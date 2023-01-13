using BlazorAlarmClock.Client.Services;
using BlazorAlarmClock.Shared.Models;
using MudBlazor;

namespace BlazorAlarmClock.Client;

public partial class MainLayout
{
    private MudTheme theme = new();
    private bool isDarkMode = true;

    private AlarmDto newAlarm= new();

    bool isOpen;
    private void CancelPopover()
    {
        newAlarm = null;
        isOpen = false;
    }

    private void AddAlarmClosePopover()
    {
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
        return base.OnInitializedAsync();
    }



    private void ToggleNavPopover()
    {
        isOpen = !isOpen;
    }
}
