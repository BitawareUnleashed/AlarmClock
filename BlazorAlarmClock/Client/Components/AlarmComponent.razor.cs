using BlazorAlarmClock.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text;

namespace BlazorAlarmClock.Client.Components;

public partial class AlarmComponent
{
    [Parameter] public AlarmDto? CurrentAlarm { get; set; }

    public bool AlarmSunday { get; set; }
    public bool AlarmMonday { get; set; }
    public bool AlarmTuesday { get; set; }
    public bool AlarmWednesday { get; set; }
    public bool AlarmThursday { get; set; }
    public bool AlarmFriday { get; set; }
    public bool AlarmSaturday { get; set; }


    public string FileName { get; set; } = "audio/file1.mp3";
    public int AlarmHour { get; set; }
    public int AlarmMinute { get; set; }
    public List<int>? AlarmDays { get; set; } = new();
    public bool AlarmActive { get; set; }
    public bool IsSnoozeVisible { get; set; }


    public AlarmStatus Status { get; set; } = AlarmStatus.NONE;
    public bool IsInSnooze { get; set; } = false;



    private int snoozing = 0;

    private int today = -1;


    TimeSpan? time = new TimeSpan(00, 45, 00);

    protected override Task OnInitializedAsync()
    {
        AlarmSunday = false;
        AlarmMonday= false;
        AlarmTuesday= false;
        AlarmWednesday= false;
        AlarmThursday= false;
        AlarmFriday= false;
        AlarmSaturday= false;

        if (CurrentAlarm is not null && CurrentAlarm.AlarmDays is not null)
        {
            foreach (var day in CurrentAlarm.AlarmDays)
            {
                AlarmDays?.Add(day.DayAsInt);
                switch (day.DayAsInt)
                {
                    case 0:
                        AlarmSunday = true;
                        break;
                    case 1:
                        AlarmMonday = true;
                        break;
                    case 2:
                        AlarmTuesday = true;
                        break;
                    case 3:
                        AlarmWednesday = true;
                        break;
                    case 4:
                        AlarmThursday = true;
                        break;
                    case 5:
                        AlarmFriday = true;
                        break;
                    case 6:
                        AlarmSaturday = true;
                        break;
                    default:
                        break;
                }
            }
        }

        SystemWatch.SecondChangedEvent += SystemWatch_SecondChangedEvent;
        Status = AlarmStatus.STOPPED;
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
                if (time is null)
                {
                    break;
                }
                if (e.Hour == time.Value.Hours && e.Minute == (time.Value.Minutes + (2 * snoozing)))
                {
                    PlaySound();
                    IsSnoozeVisible = true;
                    Status = AlarmStatus.PLAYING;
                    StateHasChanged();
                }
                break;
            case AlarmStatus.STOPPED:
                if (time is null)
                {
                    break;
                }
                if (e.Hour == time.Value.Hours && e.Minute == time.Value.Minutes)
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
                if (time is null)
                {
                    break;
                }
                if (today != e.Day || (e.Hour == time.Value.Hours && e.Minute != time.Value.Minutes))
                {
                    today = e.Day;
                    Status = AlarmStatus.STOPPED;
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
