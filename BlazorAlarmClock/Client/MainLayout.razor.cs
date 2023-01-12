using MudBlazor;

namespace BlazorAlarmClock.Client;

public partial class MainLayout
{
    private MudTheme theme = new();
    private bool isDarkMode = true;

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
}
