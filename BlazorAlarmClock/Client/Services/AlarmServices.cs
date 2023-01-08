using static System.Net.WebRequestMethods;

namespace BlazorAlarmClock.Client.Services;

public class AlarmServices
{
    private const string AlarmListEndpoint = "/api/v1/GetAlarmList";
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
}
