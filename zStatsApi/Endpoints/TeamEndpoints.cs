using zStatsApi.Dtos.Team;

namespace zStatsApi.Endpoints;

public static class TeamEndpoints
{
    const string GetTeamEndpointName = "GetTeam";

    private static readonly List<TeamDto> teams =
    [
        new(1, "The Holy Trinity"),
        new(2, "Double Trouble")
    ];

    public static WebApplication MapTeamEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/teams")
            .WithParameterValidation();
        
        // GET /teams
        group.MapGet("/", () => teams);
        
        // GET /teams/id
        group.MapGet("/{id}", (int id) => teams.Find(team => team.Id == id))
            .WithName(GetTeamEndpointName);

        // POST /teams
        group.MapPost("/", (CreateTeamDto newTeam) =>
        {
            TeamDto team = new(
                teams.Count + 1,
                newTeam.Name
            );
            
            teams.Add(team);
            return Results.CreatedAtRoute(GetTeamEndpointName, new { id = team.Id }, team);
        });

        // PUT /teams/id
        group.MapPut("/{id}", (int id, UpdateTeamDto updatedTeam) =>
        {
            var index = teams.FindIndex(team => team.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }

            teams[index] = new TeamDto(
                id,
                updatedTeam.Name
            );
            
            return Results.NoContent();
        });

        // DELETE /teams/id
        group.MapDelete("/{id}", (int id) =>
        {
            teams.RemoveAll(team => team.Id == id);
            return Results.NoContent();
        });

        return app;
    }
}