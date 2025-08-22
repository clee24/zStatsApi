using zStatsApi.Dtos.PlayerStats;

namespace zStatsApi.Endpoints;

public static class PlayerStatEndpoints
{
    const string GetPlayerStatEndpointName = "GetPlayerStat";

    private static readonly List<PlayerStatDto> playerstat =
    [
        new(1, 1, 1, 5, 2, 10, 2, 1, 8, 3, 0, 15, 1, 4, 1),
        new(2, 2, 1, 3, 1, 8, 1, 2, 6, 2, 1, 12, 0, 5, 0)
    ];

    public static WebApplication MapPlayerStatEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/playerstat")
            .WithParameterValidation();
        
        // GET /playerstat
        group.MapGet("/", () => playerstat);

        // GET /playerstat/{id}
        group.MapGet("/{id}", (int id) => playerstat.Find(stat => stat.Id == id))
            .WithName(GetPlayerStatEndpointName);

        // POST /playerstat
        group.MapPost("/", (CreatePlayerStatDto newStat) =>
        {
            PlayerStatDto stat = new(
                playerstat.Count + 1,
                newStat.PlayerId,
                newStat.SetId,
                newStat.HittingKills,
                newStat.HittingErrors,
                newStat.HittingAttempts,
                newStat.ServiceAces,
                newStat.ServiceErrors,
                newStat.ServiceAttempts,
                newStat.SettingDimes,
                newStat.SettingErrors,
                newStat.SettingAttempts,
                newStat.Blocks,
                newStat.Digs,
                newStat.Shanks
            );

            playerstat.Add(stat);
            
            return Results.CreatedAtRoute(GetPlayerStatEndpointName, new { id = stat.Id }, stat);
        });

        // PUT /playerstat/{id}
        group.MapPut("/{id}", (int id, UpdatePlayerStatDto updatedStat) =>
        {
            var index = playerstat.FindIndex(s => s.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }
            
            var existingStat = playerstat[index];

            playerstat[index] = existingStat with
            {
                HittingKills = updatedStat.HittingKills,
                HittingErrors = updatedStat.HittingErrors,
                HittingAttempts = updatedStat.HittingAttempts,
                ServiceAces = updatedStat.ServiceAces,
                ServiceErrors = updatedStat.ServiceErrors,
                ServiceAttempts = updatedStat.ServiceAttempts,
                SettingDimes = updatedStat.SettingDimes,
                SettingErrors = updatedStat.SettingErrors,
                SettingAttempts = updatedStat.SettingAttempts,
                Blocks = updatedStat.Blocks,
                Digs = updatedStat.Digs,
                Shanks = updatedStat.Shanks
            };
            
            return Results.NoContent();
        });

        // DELETE /playerstat/{id}
        group.MapDelete("/{id}", (int id) =>
        {
            playerstat.RemoveAll(s => s.Id == id);
            return Results.NoContent();
        });

        return app;
    }
}