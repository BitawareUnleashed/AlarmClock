using Microsoft.JSInterop;
using MudBlazor;

namespace BlazorAlarmClock.Client.Pages;

public partial class Index
{
    public List<string> Alarms { get; set; } = new List<string>();

    private bool alarmActivated = false;

    public bool InSnooze { get; set; }

    public bool IsSetSnooze { get; set; }


    protected override void OnInitialized()
    {
        alarmService.OnAlarmUpdated += AlarmService_OnAlarmUpdated;
        alarmService.OnAlarmDeleted += AlarmService_OnAlarmDeleted;
        base.OnInitialized();
    }

    private void AlarmService_OnAlarmDeleted(object? sender, bool e) => StateHasChanged();

    private void AlarmService_OnAlarmUpdated(object? sender, bool e)
    {
        StateHasChanged();
    }

    private void Snooze()
    {
        InSnooze = false;
        IsSetSnooze = true;
        StateHasChanged();
    }

    protected async void FullScreenMode()
    {
        await Task.Delay(5000);
        await JsRuntime.InvokeVoidAsync("enableFullScreen");
    }


    private async void Full()
    {
        await JsRuntime.InvokeVoidAsync("enableFullScreen");
        alarmActivated = true;
        StateHasChanged();
    }

    private void AlarmPlaying(bool value)
    {
        if (value)
        {
            InSnooze = value;
        }
        
        StateHasChanged();
    }

    //public void AddAlarmTest()
    //{
    //    alarmService.AddNewAlarm();
    //}
}
