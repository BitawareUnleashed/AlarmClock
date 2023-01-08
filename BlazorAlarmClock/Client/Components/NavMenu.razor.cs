using Microsoft.AspNetCore.Components;

namespace BlazorAlarmClock.Client.Components;

public partial class NavMenu
{
    [Parameter]
    public bool CollapsedMenu
    {
        get => collapseNavMenu;
        set => collapseNavMenu = value;
    }

    private bool collapseNavMenu = true;


    private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

    private void ToggleNavMenu()
    {
        collapseNavMenu = !collapseNavMenu;
    }
}
