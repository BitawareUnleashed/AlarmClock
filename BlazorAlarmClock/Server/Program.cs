using BlazorAlarmClock.Server.Controllers;
using BlazorAlarmClock.Server.Extensions;
using BlazorAlarmClock.Server.Models;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();



builder.Services.AddDbContext<AlarmDbContext>(opt =>
    opt.UseSqlite(@$"Data Source = Alarms.sqlite"));


builder.Services.AddScoped<DbContext, AlarmDbContext>();
builder.Services.AddScoped<Alarms>();

builder.Services.AddScoped(typeof(IRepository<,>), typeof(AlarmDataRepository<,>));

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

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
