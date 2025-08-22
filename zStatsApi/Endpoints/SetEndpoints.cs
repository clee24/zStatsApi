using zStatsApi.Dtos.Set;

namespace zStatsApi.Endpoints;

public static class SetEndpoints
{
    const string GetSetEndpointName = "GetSet";

    private static readonly List<SetDto> sets =
    [
        new(1, 1, 1, 25, 23, 1),
        new(2, 1, 2, 21, 25, 2),
        new(3, 1, 3, 15, 4, 1)
    ];

    public static WebApplication MapSetEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/sets")
            .WithParameterValidation();
        
        // GET /sets
        group.MapGet("/", () => sets);

        // GET /sets/{id}
        group.MapGet("/{id}", (int id) => sets.Find(set => set.Id == id)
        ).WithName(GetSetEndpointName);

        // POST /sets
        group.MapPost("/", (CreateSetDto newSet) =>
        {
            SetDto set = new(
                sets.Count + 1,
                newSet.MatchId,
                newSet.SetNumber,
                newSet.TeamAScore,
                newSet.TeamBScore,
                null
            );

            sets.Add(set);
            return Results.CreatedAtRoute(GetSetEndpointName, new { id = set.Id }, set);
        });

        // PUT /sets/{id}
        group.MapPut("/{id}", (int id, UpdateSetDto updatedSet) =>
        {
            var index = sets.FindIndex(s => s.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }

            sets[index] = sets[index] with
            {
                SetNumber = updatedSet.SetNumber,
                TeamAScore = updatedSet.TeamAScore,
                TeamBScore = updatedSet.TeamBScore,
                WinnerTeamId = updatedSet.WinnerTeamId
            };

            return Results.NoContent();
        });

        // DELETE /sets/{id}
        group.MapDelete("/{id}", (int id) =>
        {
            sets.RemoveAll(s => s.Id == id);
            return Results.NoContent();
        });

        return app;
    }
}