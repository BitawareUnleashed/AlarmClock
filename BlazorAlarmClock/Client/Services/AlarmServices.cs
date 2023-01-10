using System.Net.Http.Json;
using System.Text.Json;
using BlazorAlarmClock.Shared.Models;

namespace BlazorAlarmClock.Client.Services;

public class AlarmServices
{
    private const string AlarmListEndpoint = "/api/v1/GetAlarmList";
    private const string AddNewAlarmEndpoint = "/api/v1/AddNewAlarm";
    private readonly HttpClient http;

    public AlarmServices(HttpClient http)
    {
        this.http = http;
    }


    public async void GetAlarmList()
    {
        var ret = await http.GetAsync(@$"{AlarmListEndpoint}");
        if (!ret.IsSuccessStatusCode)
        {
            // TODO: Show a message on the screen
        }
    }

    public async void AddNewAlarm()
    {
        var alm = new Alarm()
        {
            Hour = 10,
            Minute = 15,
            IsActive = true,
            RingtoneName = string.Empty,
            AlarmDays = new List<AlarmDay>()
            {
                new AlarmDay()
                {
                    DayAsInt = 1
                },
                new AlarmDay()
                {
                    DayAsInt = 2
                }
            }
        };

        using var response = await http.PostAsJsonAsync(@$"{AddNewAlarmEndpoint}", alm);
        if (!response.IsSuccessStatusCode)
        {
            // set error message for display, log to console and return
            var errorMessage = response.ReasonPhrase;
            Console.WriteLine($"There was an error! {errorMessage}");
            return;
        }
    }
}
