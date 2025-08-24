using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using zStatsApi.Data;
using zStatsApi.Startup;
using zStatsApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var connString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION")
                 ?? throw new InvalidOperationException("POSTGRES_CONNECTION not set");

builder.Services.AddDbContext<ZStatsContext>(options =>
    options.UseNpgsql(connString));

builder.AddDependencies();

var app = builder.Build();

app.UseOpenApi();
app.UseHttpsRedirection();

app.MapPlayerEndpoints();
app.MapTeamEndpoints();
app.MapSetEndpoints();
app.MapMatchEndpoints();
app.MapPlayerStatEndpoints();

app.MigrateDb();

app.Run();