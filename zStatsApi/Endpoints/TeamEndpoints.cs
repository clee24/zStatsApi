using Microsoft.EntityFrameworkCore;
using zStatsApi.Data;
using zStatsApi.Dtos.Team;
using zStatsApi.Entities;
using zStatsApi.Mapping;

namespace zStatsApi.Endpoints;

public static class TeamEndpoints
{
    const string GetTeamEndpointName = "GetTeam";

    public static WebApplication MapTeamEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/teams")
            .WithParameterValidation();
        
        // GET /teams
        group.MapGet("/", (ZStatsContext dbContext) => 
            dbContext.Teams
                .Select(team => team.ToDto()));
        
        // GET /teams/id
        group.MapGet("/{id}", (int id, ZStatsContext dbContext) =>
            {
                var team = dbContext.Teams.Find(id);
                
                return team is null ?
                    Results.NotFound() : Results.Ok(team.ToDto());
            })
            .WithName(GetTeamEndpointName);

        // POST /teams
        group.MapPost("/", (CreateTeamDto newTeam, ZStatsContext dbContext) =>
        {
            Team team = newTeam.ToEntity();
            
            dbContext.Teams.Add(team);
            dbContext.SaveChanges();
            
            return Results.CreatedAtRoute(
                GetTeamEndpointName,
                new { id = team.Id },
                team.ToDto()
            );
        });

        // PUT /teams/id
        group.MapPut("/{id}", (int id, UpdateTeamDto updatedTeam, ZStatsContext dbContext) =>
        {
            var existingTeam = dbContext.Teams.Find(id);

            if (existingTeam is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingTeam)
                .CurrentValues
                .SetValues(updatedTeam.ToEntity(id));
            
            dbContext.SaveChanges();
            
            return Results.NoContent();
        });

        // DELETE /teams/id
        group.MapDelete("/{id}", (int id, ZStatsContext dbContext) =>
        {
            dbContext.Teams
                .Where(team => team.Id == id)
                .ExecuteDelete();
                
            dbContext.SaveChanges();
            
            return Results.NoContent();
        });

        return app;
    }
}