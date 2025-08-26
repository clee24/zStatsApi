using Microsoft.EntityFrameworkCore;
using zStatsApi.Data;
using zStatsApi.Dtos.Match;
using zStatsApi.Entities;
using zStatsApi.Mapping;
using zStatsApi.Services;

namespace zStatsApi.Endpoints;

public static class MatchEndpoints
{
    const string GetMatchEndpointName = "GetMatch";
    
    public static WebApplication MapMatchEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/matches");
        
        // GET /matches
        group.MapGet("/", (ZStatsContext dbContext) => 
            dbContext.Matches
                .Select(match => match.ToDto()));

        // GET /matches/{id}
        group.MapGet("/{id}", (int id, ZStatsContext dbContext) =>
        {
            var match = dbContext.Matches.Find(id);
            
            return match is null ?
                Results.NotFound() : Results.Ok(match.ToDto());
        })
        .WithName(GetMatchEndpointName);

        // POST /matches
        group.MapPost("/", (CreateMatchDto newMatch, CreateMatchSetService service) =>
        {
            try
            {
                var match = service.CreateMatchAndInitialSet(newMatch);
                return Results.Created($"/matches/{match.Id}", match.ToDto());
            }
            catch (ArgumentException ex)
            { return Results.BadRequest(ex.Message);
            }
        });

        // PUT /matches/{id}
        group.MapPut("/{id}", (int id, UpdateMatchDto updatedMatch, ZStatsContext dbContext) =>
        {
            var existingMatch = dbContext.Matches.Find(id);

            if (existingMatch is null)
            {
                return Results.NotFound();
            }
            
            dbContext.Entry(existingMatch)
                .CurrentValues
                .SetValues(updatedMatch.ToEntity(id));
            
            dbContext.SaveChanges();
            
            return Results.NoContent();
        });
        
        // PUT /matches/{id} with winner
        group.MapPut("/{id}", (int id, UpdateMatchDto updatedMatch, ZStatsContext dbContext) =>
        {
            var existingMatch = dbContext.Matches.Find(id);

            if (existingMatch is null)
            {
                return Results.NotFound();
            }
            
            dbContext.Entry(existingMatch)
                .CurrentValues
                .SetValues(updatedMatch.ToEntity(id));
            
            dbContext.SaveChanges();
            
            return Results.NoContent();
        });

        // DELETE /matches/{id}
        group.MapDelete("/{id}", (int id, ZStatsContext dbContext) =>
        {
            dbContext.Matches
                .Where(match => match.Id == id)
                .ExecuteDelete();
            
            dbContext.SaveChanges();
            
            return Results.NoContent();
        });

        return app;
    }
}