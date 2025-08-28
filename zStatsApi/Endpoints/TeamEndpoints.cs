using Microsoft.EntityFrameworkCore;
using zStatsApi.Data;
using zStatsApi.Dtos.Team;
using zStatsApi.Entities;
using zStatsApi.Mapping;
using zStatsApi.Services;

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
        {
            var teams = dbContext.Teams
                .Include(t => t.TeamPlayers)
                .ThenInclude(tp => tp.Player)
                .ToList(); 
            
            var dtos = teams.Select(team => team.ToDto()).ToList();

            return dtos;
        });
        
        // GET /teams/id
        group.MapGet("/{id}", async (int id, ZStatsContext dbContext) =>
            {
                var team = await dbContext.Teams
                    .Include(t => t.TeamPlayers)
                    .FirstOrDefaultAsync(t => t.Id == id);

                return team is null ?
                    Results.NotFound() : Results.Ok(team.ToDto());
            })
            .WithName(GetTeamEndpointName);

        // POST /teams
        group.MapPost("/", async (CreateTeamDto dto, TeamService service) =>
        {
            var team = await service.CreateTeamAsync(dto);
            return Results.Created($"/teams/{team.Id}", team.ToDto());
        });

        // PUT /teams/id
        group.MapPut("/{id}", async (int id, UpdateTeamDto dto, TeamService service) =>
        {
            try
            {
                await service.UpdateTeamAsync(id, dto);
                return Results.NoContent();
            }
            catch (ArgumentException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        // DELETE /teams/id
        group.MapDelete("/{id}", async (int id, TeamService service) =>
        {
            try
            {
                await service.DeleteTeamAsync(id);
                return Results.NoContent();
            }
            catch (ArgumentException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        return app;
    }
}