using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using zStatsApi.Data;
using zStatsApi.Startup;
using zStatsApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = JsonSerializer.Deserialize<Dictionary<string,string>>(
    File.ReadAllText("creds.json"))!["ZStatsApi"];

builder.Services.AddDbContext<ZStatsContext>(options =>
    options.UseNpgsql(connString));

builder.AddDependencies();

var app = builder.Build();

app.MapPlayerEndpoints();
app.MapTeamEndpoints();
app.MapSetEndpoints();
app.MapMatchEndpoints();

app.MapPlayerStatEndpoints();

app.UseOpenApi();

app.UseHttpsRedirection();

app.Run();