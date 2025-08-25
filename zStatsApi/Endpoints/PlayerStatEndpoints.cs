using Microsoft.EntityFrameworkCore;
using zStatsApi.Data;
using zStatsApi.Dtos.PlayerStats;
using zStatsApi.Entities;
using zStatsApi.Mapping;

namespace zStatsApi.Endpoints;

public static class PlayerStatEndpoints
{
    const string GetPlayerStatEndpointName = "GetPlayerStat";
    
    public static WebApplication MapPlayerStatEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/playerstat")
            .WithParameterValidation();
        
        // GET /playerstat
        group.MapGet("/", (ZStatsContext dbContext) =>
            dbContext.PlayerStats
                .Select(stat => stat.ToDto()));

        // GET /playerstat/{id}
        group.MapGet("/{id}", (int id, ZStatsContext dbContext) =>
            {
                var stat = dbContext.PlayerStats.Find(id);
                
                return stat is null ?
                    Results.NotFound() : Results.Ok(stat.ToDto());
            })
            .WithName(GetPlayerStatEndpointName);

        // POST /playerstat
        group.MapPost("/", (CreatePlayerStatDto newStat, ZStatsContext dbContext) =>
        {
            PlayerStat stat = newStat.ToEntity();
            
            dbContext.PlayerStats.Add(stat);
            dbContext.SaveChanges();
            
            return Results.CreatedAtRoute(
                GetPlayerStatEndpointName,
                new { id = stat.Id },
                stat.ToDto()
            );
        });

        // PUT /playerstat/{id}
        group.MapPut("/{id}", (int id, UpdatePlayerStatDto updatedStat, ZStatsContext dbContext) =>
        {
            var existingStat = dbContext.PlayerStats.Find(id);

            if (existingStat is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingStat)
                .CurrentValues
                .SetValues(updatedStat.ToEntity(id, existingStat.PlayerId, existingStat.TeamId, existingStat.SetId));
            
            dbContext.SaveChanges();
            
            return Results.NoContent();
        });

        // DELETE /playerstat/{id}
        group.MapDelete("/{id}", (int id, ZStatsContext dbContext) =>
        {
            dbContext.PlayerStats
                .Where(s => s.Id == id)
                .ExecuteDelete();
            
            dbContext.SaveChanges();
            
            return Results.NoContent();
        });

        return app;
    }
}