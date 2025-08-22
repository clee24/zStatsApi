using TheSampleApi.Startup;
using zStatsApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

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