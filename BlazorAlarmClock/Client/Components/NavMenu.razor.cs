using Microsoft.AspNetCore.Components;

namespace BlazorAlarmClock.Client.Components;

public partial class NavMenu
{
    [Parameter] public bool IsPopoverOpen { get; set; }

    [Parameter] public EventCallback IsPopoverOpenChanged { get; set; }

    [Parameter] public bool CollapsedMenu
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

    public void TogglePopover()
    {
        IsPopoverOpenChanged.InvokeAsync(this);
    }
}
