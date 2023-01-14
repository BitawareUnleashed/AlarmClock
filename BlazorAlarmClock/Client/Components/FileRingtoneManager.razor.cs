using Microsoft.AspNetCore.Components;

namespace BlazorAlarmClock.Client.Components;

public partial class FileRingtoneManager
{
    [Parameter][EditorRequired] public string FileName { get; set; } = string.Empty;

    private async void DeleteRingtone() => await alarmService.DeleteRingtone(FileName);
}
