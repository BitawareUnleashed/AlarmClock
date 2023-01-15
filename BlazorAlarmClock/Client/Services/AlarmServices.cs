using System.Net.Http.Json;
using System.Text.Json;
using BlazorAlarmClock.Shared.Models;
using Microsoft.AspNetCore.Components.Forms;
using static System.Net.WebRequestMethods;

namespace BlazorAlarmClock.Client.Services;

public class AlarmServices
{
    private const string AlarmListEndpoint = "/api/v1/GetAlarmList";
    private const string AddNewAlarmEndpoint = "/api/v1/AddNewAlarm";
    private const string DeleteAlarmEndpoint = "/api/v1/DeleteAlarm";
    private const string UpdateAlarmEndpoint = "/api/v1/UpdateAlarm";
    private readonly string UploadFileRingtoneEndpoint = "/api/v1/UploadRingtone";
    private readonly string GetRingtoneListEndpoint = "/api/v1/GetRingtonesList";
    private readonly string DeleteAlarmRingtoneEndpoint = "/api/v1/DeleteAlarmRingtone";

    private readonly HttpClient http;

    public event EventHandler<bool>? OnAlarmDeleted;
    public event EventHandler<bool>? OnAlarmUpdated;
    public event EventHandler<bool>? OnRingtoneListUpdated;
    public event EventHandler<string>? OnRingtoneUploaded;
    public event EventHandler<string>? OnErrorRaised;


    public List<AlarmDto> AlarmList { get; set; } = new();
    public List<string> RingtonesList { get; set; } = new();


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
            OnErrorRaised?.Invoke(this, $"{response.StatusCode} - {response.ReasonPhrase}");
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
            OnErrorRaised?.Invoke(this, $"{response.StatusCode} - {response.ReasonPhrase}");
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
            OnErrorRaised?.Invoke(this, $"{response.StatusCode} - {response.ReasonPhrase}");
            return;
        }
        var a = await GetAlarmList();
        OnAlarmUpdated?.Invoke(this, true);
    }

    public async void UpdateItem(AlarmDto day)
    {
        using var response = await http.PostAsJsonAsync(@$"{UpdateAlarmEndpoint}", day);
        if (!response.IsSuccessStatusCode)
        {
            // set error message for display, log to console and return
            var errorMessage = response.ReasonPhrase;
            Console.WriteLine($"There was an error! {errorMessage}");
            OnErrorRaised?.Invoke(this, $"{response.StatusCode} - {response.ReasonPhrase}");
            return;
        }
        var a = await GetAlarmList();
        OnAlarmUpdated?.Invoke(this, true);
    }

    public async Task<AlarmDto>? GetAlarmFromId(int id)
    {
        AlarmDto? ret = null;
        if (await GetAlarmList())
        {
            ret = AlarmList.Where(x => x.Id == id).FirstOrDefault();
        }
        return ret;
    }


    public async void UploadFiles(IBrowserFile file)
    {
        try
        {
            if (file != null)
            {
                var ms = new MemoryStream();
                await file.OpenReadStream(maxAllowedSize: long.MaxValue).CopyToAsync(ms);
                var fileBytes = ms.ToArray();

                var fileData = new FileData()
                {
                    FileName = file.Name,
                    DataBytes = fileBytes,
                    Path = "wwwroot/audio"
                };

                var response = await http.PostAsJsonAsync($"{UploadFileRingtoneEndpoint}", fileData);

                if (!response.IsSuccessStatusCode)
                {
                    OnErrorRaised?.Invoke(this, $"{response.StatusCode} - {response.ReasonPhrase}");
                }
                else
                {
                    await GetRingroneList();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        OnRingtoneUploaded?.Invoke(this, file.Name);
    }

    public async Task<bool> GetRingroneList()
    {
        var ret = false;

        var response = await http.GetAsync(@$"{GetRingtoneListEndpoint}");
        if (!response.IsSuccessStatusCode)
        {
            // set error message for display, log to console and return
            var errorMessage = response.ReasonPhrase;
            Console.WriteLine($"There was an error in GetAlarmList! {errorMessage}");
            OnErrorRaised?.Invoke(this, $"{response.StatusCode} - {response.ReasonPhrase}");
            return ret;
        }

        RingtonesList = await response.Content.ReadFromJsonAsync<List<string>>() ?? new List<string>();

        return response.IsSuccessStatusCode;
    }

    public async Task DeleteRingtone(string fileName)
    {
        var response = await http.PostAsJsonAsync($"{DeleteAlarmRingtoneEndpoint}", fileName);

        if (!response.IsSuccessStatusCode)
        {
            OnErrorRaised?.Invoke(this, $"{response.StatusCode} - {response.ReasonPhrase}");
        }
        await GetRingroneList();
        OnRingtoneListUpdated?.Invoke(this, true);
    }


}
