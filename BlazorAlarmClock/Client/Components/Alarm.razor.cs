using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text;

namespace BlazorAlarmClock.Client.Components;

public partial class Alarm
{

    public enum AlarmStatus
    {
        NONE = 0x00,
        PLAYING = 0x01,
        SNOOZED = 0x02,
        STOPPED = 0x03,
        STOPPED_TODAY = 0x04

    }



    [Parameter][EditorRequired] public string FileName { get; set; } = "audio/file1.mp3";
    [Parameter]public int AlarmHour { get; set; }
    [Parameter]public int AlarmMinute { get; set; }
    [Parameter] public List<int>? AlarmDays { get; set; }
    [Parameter][EditorRequired] public bool AlarmActive { get; set; }
    [Parameter] public bool IsSnoozeVisible { get; set; }

    public AlarmStatus Status { get; set; }= AlarmStatus.NONE;

    public bool IsInSnooze { get; set; } = false;

    private int snoozing = 0;

    private int today = -1;

    protected override Task OnInitializedAsync()
    {
        SystemWatch.SecondChangedEvent += SystemWatch_SecondChangedEvent;
        Status= AlarmStatus.STOPPED;
        return base.OnInitializedAsync();
    }

    private void SystemWatch_SecondChangedEvent(object? sender, DateTime e)
    {
        switch (Status)
        {
            case AlarmStatus.NONE:
                break;
            case AlarmStatus.PLAYING:
                break;
            case AlarmStatus.SNOOZED:
                if (e.Hour == AlarmHour && e.Minute == (AlarmMinute + (2 * snoozing)))
                {
                    PlaySound();
                    IsSnoozeVisible = true;
                    Status = AlarmStatus.PLAYING;
                    StateHasChanged();
                }
                break;
            case AlarmStatus.STOPPED:
                if (e.Hour == AlarmHour && e.Minute == AlarmMinute)
                {
                    if (AlarmDays is null)
                    {
                        IsSnoozeVisible = true;
                        PlaySound();
                    }
                    else
                    {
                        if (AlarmDays.Contains(e.Day))
                        {
                            IsSnoozeVisible = true;
                            PlaySound();
                        }
                    }
                    StateHasChanged();
                }
                break;
            case AlarmStatus.STOPPED_TODAY:
                if(today != e.Day || (e.Hour == AlarmHour && e.Minute != AlarmMinute))
                {
                    today = e.Day;
                    Status= AlarmStatus.STOPPED;
                }
                break;
            default:
                break;
        }
    }

    private void SnoozeToggle()
    {
        IsSnoozeVisible = !IsSnoozeVisible;
    }

    private async void PlaySound()
    {
        Status = AlarmStatus.PLAYING; 
        await JsRuntime.InvokeVoidAsync("PlaySound");
    }


    private async void StopSound()
    {
        snoozing = 0;
        await JsRuntime.InvokeVoidAsync("StopSound");
    }

    private async void SnoozeSound()
    {
        Status = AlarmStatus.SNOOZED;
        IsSnoozeVisible = false;
        snoozing++;
        await JsRuntime.InvokeVoidAsync("StopSound");
    }

    private async void StopAlarm()
    {
        Status = AlarmStatus.STOPPED_TODAY;
        today = DateTime.Today.Day;
        IsSnoozeVisible = false;
        snoozing = 0;
        await JsRuntime.InvokeVoidAsync("StopSound");
    }
}
