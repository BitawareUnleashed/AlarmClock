using BlazorAlarmClock.Server.Controllers;
using BlazorAlarmClock.Server.Extensions;
using BlazorAlarmClock.Server.Models;
using BlazorAlarmClock.Shared.Models;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Weather;
using Weather.Business;
using Weather.Business.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.Configure<OpenWeatherMapKey>(options => builder.Configuration.GetSection("OpenWeatherMapKey").Bind(options));

builder.Services.AddDbContext<AlarmDbContext>(opt =>
    opt.UseSqlite(@$"Data Source = Alarms.sqlite"));



builder.Services.AddScoped<DbContext, AlarmDbContext>();
builder.Services.AddScoped<Alarms>();

builder.Services.AddScoped<AlarmDataRepository>();

builder.Services.AddWeatherBusiness();

var app = builder.Build();

await app.InizializeDatabases();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.AddAlarmsApiEndpoints();

app.UseWeather();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
