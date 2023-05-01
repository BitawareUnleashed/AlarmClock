using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Weather.Entities.Models;
using static System.Net.WebRequestMethods;

namespace Weather.Services;

public class OpenWeatherService
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly HttpClient http;
    string api_url = string.Empty;
    private string baseAddress = "https://openweathermap.org";
    private string baseApiAddress = "https://api.openweathermap.org";
    private const string WeatherSaveLocationEndpoint = "/api/v2/PostSaveLocation";
    private const string WeatherSaveLocEndpoint = "/api/v1/PostSaveLocation";
    private string iconAddress = "/img/wn/";
    private string weatherAddress = "/data/2.5/weather";

    private const string WeatherApiKeyEndpoint = "/api/v1/GetApiKey";
    private const string WeatherLocationsEndpoint = "/api/v1/GetWeatherLocations";
    private const string WeatherSingleLocationsEndpoint = "/api/v1/GetSingle";
    private const string WeatherGetLocationEndpoint = "/api/v1/GetSavedLocation";

    /// <summary>
    /// Gets or sets the weather locations.
    /// </summary>
    /// <value>
    /// The weather locations.
    /// </value>
    public List<string> WeatherLocations { get; set; } = new();

    /// <summary>
    /// Occurs when an error raises.
    /// </summary>
    public event EventHandler<string>? OnErrorRaised;
    

    /// <summary>
    /// Gets or sets the open weather map API key.
    /// </summary>
    /// <value>
    /// The open weather map API key.
    /// </value>
    public string? OpenWeatherMapApiKey { get; private set; }

    /// <summary>
    /// Gets the meteo pack.
    /// </summary>
    /// <value>
    /// The meteo pack.
    /// </value>
    public MeteoPack? MeteoPack { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="OpenWeatherService" /> class.
    /// </summary>
    /// <param name="httpClientFactory">The HTTP client factory.</param>
    /// <param name="http">The HTTP.</param>
    public OpenWeatherService(IHttpClientFactory httpClientFactory, HttpClient http)
    {
        this.httpClientFactory = httpClientFactory;
        this.http = http;
        //Task.Run(async () => { OpenWeatherMapApiKey = await GetApiKey(); });
        Initialize();
    }

    /// <summary>
    /// Initializes this instance.
    /// </summary>
    private async void Initialize()
    {
        OpenWeatherMapApiKey = await GetApiKey();
    }
    
    /// <summary>
    /// Updates the specified latitude.
    /// </summary>
    /// <param name="latitude">The latitude.</param>
    /// <param name="longitude">The longitude.</param>
    /// <param name="apiKey">The API key.</param>
    /// <returns></returns>
    public async Task<MeteoPack?> Update(double latitude, double longitude, string apiKey)
    {
        try
        {
            if (latitude == 0 && longitude == 0)
            {
                throw new ArgumentOutOfRangeException("No coordinates");
            }

            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentOutOfRangeException("Invalid ApiKey Key");
            }

            // Call to API
            api_url = $@"{baseApiAddress}{weatherAddress}?lat={latitude.ToString().Replace(",", ".")}&lon={longitude.ToString().Replace(",", ".")}&units=metric&appid={apiKey}";


            // Open connection
            var client = httpClientFactory.CreateClient("openweathermap");
            using var weatherResponse = await client.GetAsync(api_url);
            var stringResult = weatherResponse.Content.ReadAsStringAsync().Result;
            if (stringResult.Length == 0)
            {
                throw new ArgumentNullException("Empty response");
            }

            // Get json from OWM
            MeteoPack = JsonConvert.DeserializeObject<MeteoPack>(stringResult);

            return MeteoPack;
        }
        catch (HttpRequestException ex)
        {
            return null;
        }
        catch (ArgumentNullException ex)
        {
            return null;
        }
        catch (ArgumentOutOfRangeException ex)
        {
            return null;
        }
    }

    /// <summary>
    /// Gets the icon.
    /// </summary>
    /// <param name="meteo">The meteo.</param>
    /// <returns></returns>
    public string GetIcon(MeteoPack? meteo)
    {
        var resultString = string.Empty;
        try
        {
            if (meteo is null)
            {
                throw new ArgumentNullException("Meteo pack is null");
            }

            if (meteo.weather is null)
            {
                throw new ArgumentNullException("Meteo pack - weather is null");
            }

            if (meteo.weather.Length == 0)
            {
                throw new ArgumentNullException("Meteo pack - weather has no items");
            }

            if (meteo.weather.FirstOrDefault() is null)
            {
                throw new ArgumentNullException("Meteo pack: weather definition is null");
            }

            if (meteo.weather.FirstOrDefault()!.icon is null)
            {
                throw new ArgumentNullException("Meteo pack: icon name is null");
            }

            if (meteo.weather.FirstOrDefault()!.icon.Length == 0)
            {
                throw new ArgumentNullException("Meteo pack: icon name is empty");
            }

            var c = meteo.weather.FirstOrDefault()!.icon + "@2x.png";
            resultString = baseAddress + iconAddress + c;
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine(ex.Message);
        }

        return resultString;
    }

    /// <summary>
    /// Gets the locations list.
    /// </summary>
    /// <returns></returns>
    public async Task<List<string>> GetLocationsList()
    {
        var response = await http.GetAsync(@$"{WeatherLocationsEndpoint}");
        if (!response.IsSuccessStatusCode)
        {
            // set error message for display, log to console and return
            var errorMessage = response.ReasonPhrase;
            Console.WriteLine($"There was an error in GetAlarmList! {errorMessage}");
            OnErrorRaised?.Invoke(this, $"{response.StatusCode} - {response.ReasonPhrase}");
            return new List<string>();
        }

        return await response.Content.ReadFromJsonAsync<List<string>>() ?? new List<string>();
    }

    /// <summary>
    /// Gets the API key.
    /// </summary>
    /// <returns></returns>
    public async Task<string> GetApiKey()
    {
        var ret = string.Empty;

        var response = await http.GetAsync(@$"{WeatherApiKeyEndpoint}");
        if (!response.IsSuccessStatusCode)
        {
            // set error message for display, log to console and return
            var errorMessage = response.ReasonPhrase;
            Console.WriteLine($"There was an error in GetAlarmList! {errorMessage}");
            OnErrorRaised?.Invoke(this, $"{response.StatusCode} - {response.ReasonPhrase}");
            return ret;
        }

        ret = await response.Content.ReadFromJsonAsync<string>();
        OpenWeatherMapApiKey = ret ?? string.Empty;
        return OpenWeatherMapApiKey;
    }

    /// <summary>
    /// Gets the locations list.
    /// </summary>
    /// <param name="location">The location.</param>
    /// <returns></returns>
    public async Task<List<UiLocation>> GetLocationsList(string location)
    {
        try
        {
            var response = await http.GetAsync(@$"{WeatherSingleLocationsEndpoint}/{location}");
            if (!response.IsSuccessStatusCode)
            {
                // set error message for display, log to console and return
                var errorMessage = response.ReasonPhrase;
                Console.WriteLine($"There was an error in GetAlarmList! {errorMessage}");
                OnErrorRaised?.Invoke(this, $"{response.StatusCode} - {response.ReasonPhrase}");
                return new List<UiLocation>();
            }

            return await response.Content.ReadFromJsonAsync<List<UiLocation>>() ?? new List<UiLocation>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return new List<UiLocation>();
    }

    /// <summary>
    /// Saves the location.
    /// </summary>
    /// <param name="location">The location.</param>
    public async Task SaveLocation(UiLocation location)
    {
        string itemName = location.Name;
        double itemLat = location.Lat;
        double itemLong = location.Long;
        int id = location.ID;
        
        var response = await http.GetAsync(@$"{WeatherSaveLocationEndpoint}/{itemName}/{SharedMethods.Base64Encode(itemLat.ToString(CultureInfo.InvariantCulture))}/{SharedMethods.Base64Encode(itemLong.ToString(CultureInfo.InvariantCulture))}/{id}");
        //var response = await http.GetAsync(@$"{WeatherSaveLocEndpoint}/{location}");
        if (!response.IsSuccessStatusCode)
        {
            // set error message for display, log to console and return
            var errorMessage = response.ReasonPhrase;
            Console.WriteLine($"There was an error in GetAlarmList! {errorMessage}");
            OnErrorRaised?.Invoke(this, $"{response.StatusCode} - {response.ReasonPhrase}");
        }
    }

    /// <summary>
    /// Gets the saved location.
    /// </summary>
    /// <returns></returns>
    public async Task<UiLocation> GetSavedLocation()
    {
        var response = await http.GetAsync(@$"{WeatherGetLocationEndpoint}");
        if (!response.IsSuccessStatusCode)
        {
            // set error message for display, log to console and return
            var errorMessage = response.ReasonPhrase;
            Console.WriteLine($"There was an error in GetAlarmList! {errorMessage}");
            OnErrorRaised?.Invoke(this, $"{response.StatusCode} - {response.ReasonPhrase}");
        }

        var savedLocation = await response.Content.ReadFromJsonAsync<UiLocation>();
        return savedLocation;
    }
}