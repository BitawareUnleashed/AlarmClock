using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Models;

namespace Weather.Services;
public class OpenWeatherService
{
    private readonly IHttpClientFactory httpClientFactory;
    string api_url = string.Empty;
    private string baseAddress = "https://openweathermap.org";
    private string baseApiAddress = "https://api.openweathermap.org";
    private string iconAddress = "/img/wn/";
    private string weatherAddress = "/data/2.5/weather";

    /// <summary>
    /// Gets the meteo pack.
    /// </summary>
    /// <value>
    /// The meteo pack.
    /// </value>
    public MeteoPack? MeteoPack { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="OpenWeatherService"/> class.
    /// </summary>
    /// <param name="httpClientFactory">The HTTP client factory.</param>
    public OpenWeatherService(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
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

            //api_url = $@"https://api.openweathermap.org/data/2.5/weather?lat={latitude.ToString().Replace(",", ".")}&lon={longitude.ToString().Replace(",", ".")}&units=metric&appid={apiKey}";
            api_url = baseApiAddress + weatherAddress + $@"?lat={latitude.ToString().Replace(",", ".")}&lon={longitude.ToString().Replace(",", ".")}&units=metric&appid={apiKey}";

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
}