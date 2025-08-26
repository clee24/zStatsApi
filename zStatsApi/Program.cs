using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using zStatsApi.Data;
using zStatsApi.Startup;
using zStatsApi.Endpoints;
using zStatsApi.Services;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var connString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION")
                 ?? throw new InvalidOperationException("POSTGRES_CONNECTION not set");

builder.Services.AddDbContext<ZStatsContext>(options =>
    options.UseNpgsql(connString));

builder.AddDependencies();
builder.Services.AddScoped<CreateMatchSetService>();
builder.Services.AddScoped<TeamService>();

var app = builder.Build();

app.UseOpenApi();
app.UseHttpsRedirection();


app.UseStaticFiles();
app.MapGet("/", async context =>
{
    context.Response.Redirect("/index.html");
});

app.MapPlayerEndpoints();
app.MapTeamEndpoints();
app.MapSetEndpoints();
app.MapMatchEndpoints();
app.MapPlayerStatEndpoints();

app.MigrateDb();

app.Run();

// Step 1. Create the "match" 
//get request for players --> post request for players
//get request for teams ---> post request for teams OR put request for teams if the teams have changed

//if a team or player exists --> populate the frontend json of the players and teams

//ONE new Api request to create the match
//post request for match
//post request for set
