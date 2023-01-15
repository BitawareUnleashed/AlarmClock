using BlazorAlarmClock.Client.Components;
using BlazorAlarmClock.Client.Services;
using BlazorAlarmClock.Shared.Models;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace BlazorAlarmClock.Client;

public partial class MainLayout
{
    private MudTheme theme = new();
    private bool isDarkMode = true;
    bool isOpen;

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
}
