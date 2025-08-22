using zStatsApi.Dtos.PlayerStats;

namespace zStatsApi.Endpoints;

public static class PlayerStatsEndpoints
{
    const string GetPlayerStatsEndpointName = "GetPlayerStats";

    private static readonly List<PlayerStatsDto> playerstats =
    [
        new(1, 1, 1, 5, 2, 10, 2, 1, 8, 3, 0, 15, 1, 4, 1),
        new(2, 2, 1, 3, 1, 8, 1, 2, 6, 2, 1, 12, 0, 5, 0)
    ];

    public static WebApplication MapPlayerStatsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/playerstats")
            .WithParameterValidation();
        
        // GET /playerstats
        group.MapGet("/", () => playerstats);

        // GET /playerstats/{id}
        group.MapGet("/{id}", (int id) => playerstats.Find(stats => stats.Id == id))
            .WithName(GetPlayerStatsEndpointName);

        // POST /playerstats
        group.MapPost("/", (CreatePlayerStatsDto newStats) =>
        {
            PlayerStatsDto stats = new(
                playerstats.Count + 1,
                newStats.PlayerId,
                newStats.SetId,
                newStats.HittingKills,
                newStats.HittingErrors,
                newStats.HittingAttempts,
                newStats.ServiceAces,
                newStats.ServiceErrors,
                newStats.ServiceAttempts,
                newStats.SettingDimes,
                newStats.SettingErrors,
                newStats.SettingAttempts,
                newStats.Blocks,
                newStats.Digs,
                newStats.Shanks
            );

            playerstats.Add(stats);
            
            return Results.CreatedAtRoute(GetPlayerStatsEndpointName, new { id = stats.Id }, stats);
        });

        // PUT /playerstats/{id}
        group.MapPut("/{id}", (int id, UpdatePlayerStatsDto updatedStats) =>
        {
            var index = playerstats.FindIndex(s => s.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }
            
            var existingStats = playerstats[index];

            playerstats[index] = existingStats with
            {
                HittingKills = updatedStats.HittingKills,
                HittingErrors = updatedStats.HittingErrors,
                HittingAttempts = updatedStats.HittingAttempts,
                ServiceAces = updatedStats.ServiceAces,
                ServiceErrors = updatedStats.ServiceErrors,
                ServiceAttempts = updatedStats.ServiceAttempts,
                SettingDimes = updatedStats.SettingDimes,
                SettingErrors = updatedStats.SettingErrors,
                SettingAttempts = updatedStats.SettingAttempts,
                Blocks = updatedStats.Blocks,
                Digs = updatedStats.Digs,
                Shanks = updatedStats.Shanks
            };
            
            return Results.NoContent();
        });

        // DELETE /playerstats/{id}
        group.MapDelete("/{id}", (int id) =>
        {
            playerstats.RemoveAll(s => s.Id == id);
            return Results.NoContent();
        });

        return app;
    }
}