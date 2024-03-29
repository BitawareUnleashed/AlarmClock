﻿using BlazorAlarmClock.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BlazorAlarmClock.Client.Components;

public partial class AlarmComponent
{
    #region Fields

    MudTimePicker picker;

    private int snoozing = 0;

    private int today = -1;

    TimeSpan? time = new TimeSpan(00, 00, 00);

    #endregion

    #region Properties

    private const string minutesMeasureUnits = "Minutes";

    private bool isPopoverEditAlarmVisible;

    [Parameter] public AlarmDto? CurrentAlarm { get; set; }

    [Parameter] public bool? IsNewAlarm { get; set; }

    [Parameter] public EventCallback<AlarmDto> EditRequested { get; set; }

    public bool AlarmSunday { get; set; }
    public bool AlarmMonday { get; set; }
    public bool AlarmTuesday { get; set; }
    public bool AlarmWednesday { get; set; }
    public bool AlarmThursday { get; set; }
    public bool AlarmFriday { get; set; }
    public bool AlarmSaturday { get; set; }

    private void OnSundayClick() => ModifyDayList(0, !AlarmSunday);
    private void OnMondayClick() => ModifyDayList(1, !AlarmMonday);
    private void OnTuesdayClick() => ModifyDayList(2, !AlarmTuesday);
    private void OnWednesdayClick() => ModifyDayList(3, !AlarmWednesday);
    private void OnThursdayClick() => ModifyDayList(4, !AlarmThursday);
    private void OnFridayClick() => ModifyDayList(5, !AlarmFriday);
    private void OnSaturdayClick() => ModifyDayList(6, !AlarmSaturday);


    public string FileName { get; set; } = "audio/file1.mp3";
    public int AlarmHour { get; set; }
    public int AlarmMinute { get; set; }
    public bool IsSnoozeVisible { get; set; }

    public AlarmStatus Status { get; set; } = AlarmStatus.NONE;
    public bool IsInSnooze { get; set; } = false;

    #endregion

    private void ModifyDayList(int day, bool active)
    {
        if (CurrentAlarm is null)
        {
            return;
        }

        if (CurrentAlarm.AlarmDays is null)
        {
            CurrentAlarm.AlarmDays = new List<AlarmDayDto>();
        }

        var isInDay = CurrentAlarm.AlarmDays.Where(x => x.DayAsInt == day).ToList().FirstOrDefault();


        if (isInDay is null)
        {
            CurrentAlarm.AlarmDays.Add(new AlarmDayDto()
            {
                DayAsInt = day
            });
        }
        else
        {
            if (active)
            {
                isInDay.DayAsInt = day;
            }
            else
            {
                CurrentAlarm.AlarmDays.Remove(isInDay);
            }
        }
    }

    protected override Task OnInitializedAsync()
    {
        AlarmSunday = false;
        AlarmMonday = false;
        AlarmTuesday = false;
        AlarmWednesday = false;
        AlarmThursday = false;
        AlarmFriday = false;
        AlarmSaturday = false;

        if (CurrentAlarm is not null && CurrentAlarm.AlarmDays is not null)
        {
            foreach (var day in CurrentAlarm.AlarmDays)
            {
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

            AlarmHour = CurrentAlarm.Hour;
            AlarmMinute = CurrentAlarm.Minute;
            FileName = string.IsNullOrEmpty(CurrentAlarm.RingtoneName) ? "audio/file1.mp3" : CurrentAlarm.RingtoneName;
            time = new TimeSpan(CurrentAlarm.Hour, CurrentAlarm.Minute, 0);
        }

        alarmService.OnAlarmUpdated += AlarmService_OnAlarmUpdated;

        SystemWatch.SecondChangedEvent += SystemWatch_SecondChangedEvent;
        Status = AlarmStatus.STOPPED;
        return base.OnInitializedAsync();
    }

    private async void AlarmService_OnAlarmUpdated(object? sender, bool e)
    {
        var returnedAlarm = await alarmService.GetAlarmFromId(CurrentAlarm.Id);
        if (returnedAlarm is not null)
        {
            CurrentAlarm = returnedAlarm;
        }
    }

    private void AlarmTimeAccept(bool cancel = true)
    {
        picker.Close(cancel);
        if (CurrentAlarm is not null && time is not null)
        {
            CurrentAlarm.Hour = time.Value.Hours;
            CurrentAlarm.Minute = time.Value.Minutes;
            if (IsNewAlarm is null)
            {
                alarmService.UpdateItem(CurrentAlarm);
            }
        }
    }

    private void SystemWatch_SecondChangedEvent(object? sender, DateTime e)
    {
        if (CurrentAlarm is null) return;
        if (!CurrentAlarm.IsActive) return;

        switch (Status)
        {
            case AlarmStatus.NONE:
                break;
            case AlarmStatus.PLAYING:
                break;
            case AlarmStatus.SNOOZED:
                if (time is null)
                {
                    Status = AlarmStatus.STOPPED_TODAY;
                    break;
                }
                if (e.Hour == time.Value.Hours &&
                    e.Minute >= time.Value.Minutes &&
                    e.Second >= time.Value.Seconds)
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
                    if (CurrentAlarm.AlarmDays is null || CurrentAlarm.AlarmDays.Count() == 0)
                    {
                        IsSnoozeVisible = true;
                        PlaySound();
                    }
                    else
                    {
                        if (CurrentAlarm.AlarmDays.Where(x => x.DayAsInt == (int)e.DayOfWeek)
                                .FirstOrDefault() is not null)
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
            case AlarmStatus.UNDEFINED:
            case AlarmStatus.UNKNOWN:
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
        time = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute + CurrentAlarm.SnoozeDelay, DateTime.Now.Second);
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

    private void DeleteAlarm()
    {
        if (CurrentAlarm is null) return;
        alarmService.DeleteAlarm(CurrentAlarm.Id);
    }

    private async void SnoozeChanged(int val)
    {
        CurrentAlarm.SnoozeDelay = val;


        if (IsNewAlarm is null)
        {
            alarmService.UpdateItem(CurrentAlarm);
        }
    }


    private AlarmDto? selectedAlarm;

    public void EditAlarm(int id)
    {
        selectedAlarm = alarmService.AlarmList.FirstOrDefault(x => x.Id == id);
        if (selectedAlarm is not null)
        {
            EditRequested.InvokeAsync(selectedAlarm);
        }
    }
}