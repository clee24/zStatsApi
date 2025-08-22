using zStatsApi.Dtos.Player;

namespace zStatsApi.Endpoints
{
    public static class PlayerEndpoints
    {
        const string GetPlayerEndpointName = "GetPlayer";

        private  static readonly List<PlayerDto> players =
        [
            new(1, "Chris"),
            new(2, "Trinity"),
            new(3, "Zey"),
            new(4, "Jimmy")
        ];

        public static WebApplication MapPlayerEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/players")
                .WithParameterValidation();
            
            // GET /players
            group.MapGet("/", () => players);
            
            // GET /players/id
            group.MapGet("/{id}", (int id) => players.Find(player => player.Id == id))
                .WithName(GetPlayerEndpointName);

            // POST /players
            group.MapPost("/", (CreatePlayerDto newPlayer) =>
            {
                PlayerDto player = new(
                    players.Count + 1,
                    newPlayer.FullName
                );

                players.Add(player);
                return Results.CreatedAtRoute(GetPlayerEndpointName, new { id = player.Id }, player);
            });

            // PUT /players/id
            group.MapPut("/{id}", (int id, UpdatePlayerDto updatedPlayer) =>
            {
                var index = players.FindIndex(player => player.Id == id);

                if (index == -1)
                {
                    return Results.NotFound();
                }

                players[index] = new PlayerDto(
                    id,
                    updatedPlayer.FullName,
                    updatedPlayer.Nickname
                );

                return Results.NoContent();
            });

            // DELETE /players/id
            group.MapDelete("/{id}", (int id) =>
            {
                players.RemoveAll(player => player.Id == id);
                return Results.NoContent();
            });

            return app;
        }
    }
}
