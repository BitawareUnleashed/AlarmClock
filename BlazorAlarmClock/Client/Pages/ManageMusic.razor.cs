namespace BlazorAlarmClock.Client.Pages;

public partial class ManageMusic
{
    protected override Task OnInitializedAsync()
    {
        alarmService.OnRingtoneListUpdated += AlarmService_OnRingtoneListUpdated;
        return base.OnInitializedAsync();
    }

    private void AlarmService_OnRingtoneListUpdated(object? sender, bool e) =>
        StateHasChanged();

}
