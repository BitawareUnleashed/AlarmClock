using BlazorAlarmClock.Client;
using DateTimeComponent.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("BlazorAlarmClock.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlazorAlarmClock.ServerAPI"));

builder.Services.AddMudServices();


builder.Services.AddScoped<SystemWatch>(o =>
{
    SystemWatch sw = new SystemWatch(100);
    return sw;
});

await builder.Build().RunAsync();
