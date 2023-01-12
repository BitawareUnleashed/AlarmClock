using System.Net.Http.Json;
using System.Text.Json;
using BlazorAlarmClock.Shared.Models;

namespace BlazorAlarmClock.Client.Services;

public class AlarmServices
{
    private const string AlarmListEndpoint = "/api/v1/GetAlarmList";
    private const string AddNewAlarmEndpoint = "/api/v1/AddNewAlarm";
    private const string DeleteAlarmEndpoint = "/api/v1/DeleteAlarm";
    private const string UpdateAlarmEndpoint = "/api/v1/UpdateAlarm";
    private readonly HttpClient http;

    public event EventHandler<bool> OnAlarmDeleted;

    
    public List<AlarmDto> AlarmList { get; set; } = new();


    public AlarmServices(HttpClient http)
    {
        this.http = http;
    }


    public async Task<bool> GetAlarmList()
    {
        var response = await http.GetAsync(@$"{AlarmListEndpoint}");
        if (!response.IsSuccessStatusCode)
        {
            // set error message for display, log to console and return
            var errorMessage = response.ReasonPhrase;
            Console.WriteLine($"There was an error in GetAlarmList! {errorMessage}");
            return false;
        }
        AlarmList = await response.Content.ReadFromJsonAsync<List<AlarmDto>>() ?? new List<AlarmDto>();
        return true;
    }

    public async void DeleteAlarm(int alarmId)
    {
        //DeleteAlarmEndpoint
        using var response = await http.PostAsJsonAsync(@$"{DeleteAlarmEndpoint}", alarmId);
        if (!response.IsSuccessStatusCode)
        {
            // set error message for display, log to console and return
            var errorMessage = response.ReasonPhrase;
            Console.WriteLine($"There was an error in DeleteAlarm! {errorMessage}");
            return;
        }
        var a = await GetAlarmList();
        if (a)
        {
            OnAlarmDeleted?.Invoke(this, true);
        }
    }

    public async void AddNewAlarm(AlarmDto alm)
    {
        using var response = await http.PostAsJsonAsync(@$"{AddNewAlarmEndpoint}", alm);
        if (!response.IsSuccessStatusCode)
        {
            // set error message for display, log to console and return
            var errorMessage = response.ReasonPhrase;
            Console.WriteLine($"There was an error! {errorMessage}");
            return;
        }
    }

    public async void UpdateItem(AlarmDto day)
    {
        using var response = await http.PostAsJsonAsync(@$"{UpdateAlarmEndpoint}", day);
        if (!response.IsSuccessStatusCode)
        {
            // set error message for display, log to console and return
            var errorMessage = response.ReasonPhrase;
            Console.WriteLine($"There was an error! {errorMessage}");
            return;
        }
    }
}
