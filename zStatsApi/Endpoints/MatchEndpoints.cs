using zStatsApi.Dtos.Match;

namespace zStatsApi.Endpoints;

public static class MatchEndpoints
{
    const string GetMatchEndpointName = "GetMatch";

    private static readonly List<MatchDto> matches =
    [
        new(1, new DateTime(2025, 01, 15), "Whiskey Island", 1, 2, null),
        new(2, new DateTime(2025, 01, 20), "Mulberry's", 2, 1, 2)
    ];

    public static WebApplication MapMatchEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/matches");
        
        // GET /matches
        group.MapGet("/", () => matches);

        // GET /matches/{id}
        group.MapGet("/{id}", (int id) => matches.Find(match => match.Id == id))
            .WithName(GetMatchEndpointName);

        // POST /matches
        group.MapPost("/", (CreateMatchDto newMatch) =>
        {
            MatchDto match = new(
                matches.Count + 1,
                newMatch.Date,
                newMatch.Location,
                newMatch.TeamAId,
                newMatch.TeamBId,
                null
            );

            matches.Add(match);
            return Results.CreatedAtRoute(GetMatchEndpointName, new { id = match.Id }, match);
        });

        // PUT /matches/{id}
        group.MapPut("/{id}", (int id, UpdateMatchDto updatedMatch) =>
        {
            var index = matches.FindIndex(m => m.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }

            var existingMatch = matches[index];

            matches[index] = existingMatch with
            {
                Date = updatedMatch.Date,
                Location = updatedMatch.Location,
                TeamAId = updatedMatch.TeamAId,
                TeamBId = updatedMatch.TeamBId,
                WinnerTeamId = updatedMatch.WinnerTeamId
            };
            return Results.NoContent();
        });

        // DELETE /matches/{id}
        group.MapDelete("/{id}", (int id) =>
        {
            matches.RemoveAll(m => m.Id == id);
            return Results.NoContent();
        });

        return app;
    }
}