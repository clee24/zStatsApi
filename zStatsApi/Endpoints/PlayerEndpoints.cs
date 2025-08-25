using zStatsApi.Data;
using zStatsApi.Dtos.Player;
using zStatsApi.Entities;
using zStatsApi.Mapping;
using Microsoft.EntityFrameworkCore;

namespace zStatsApi.Endpoints
{
    public static class PlayerEndpoints
    {
        const string GetPlayerEndpointName = "GetPlayer";

        public static WebApplication MapPlayerEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/players")
                .WithParameterValidation();

            // GET /players
            group.MapGet("/", (ZStatsContext dbContext) =>
                dbContext.Players
                    .Select(player => player.ToDto()));

            // GET /players/id
            group.MapGet("/{id}", (int id, ZStatsContext dbContext) =>
                {
                    var player = dbContext.Players.Find(id);

                    return player is null ?
                        Results.NotFound() : Results.Ok(player.ToDto());
                })
                .WithName(GetPlayerEndpointName);

            // POST /players
            group.MapPost("/", (CreatePlayerDto newPlayer, ZStatsContext dbContext) =>
            {
                Player player = newPlayer.ToEntity();
                
                dbContext.Players.Add(player);
                dbContext.SaveChanges();

                return Results.CreatedAtRoute(
                    GetPlayerEndpointName,
                    new { id = player.Id },
                    player.ToDto()
                );
            });

            // PUT /players/id
            group.MapPut("/{id}", (int id, UpdatePlayerDto updatedPlayer, ZStatsContext dbContext) =>
            {
                var existingPlayer = dbContext.Players.Find(id);

                if (existingPlayer is null)
                {
                    return Results.NotFound();
                }

                dbContext.Entry(existingPlayer)
                    .CurrentValues
                    .SetValues(updatedPlayer.ToEntity(id));
                    
                dbContext.SaveChanges();

                return Results.NoContent();
            });

            // DELETE /players/id
            group.MapDelete("/{id}", (int id, ZStatsContext dbContext) =>
            {
                dbContext.Players
                    .Where(player => player.Id == id)
                    .ExecuteDelete();

                dbContext.SaveChanges();

                return Results.NoContent();
            });

            return app;
        }
    }
}