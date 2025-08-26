using zStatsApi.Data;
using zStatsApi.Dtos.Match;
using zStatsApi.Entities;

namespace zStatsApi.Services;

public class CreateMatchSetService
{
    private readonly ZStatsContext _dbContext;

    public CreateMatchSetService(ZStatsContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Match CreateMatchAndInitialSet(CreateMatchDto dto)
    {
        var teamA = _dbContext.Teams.Find(dto.TeamAId);
        var teamB = _dbContext.Teams.Find(dto.TeamBId);

        if (teamA == null || teamB == null)
            throw new ArgumentException("Invalid team IDs.");

        var match = new Match
        {
            Date = dto.Date,
            Location = dto.Location,
            TeamA = teamA,
            TeamB = teamB
        };

        _dbContext.Matches.Add(match);
        _dbContext.SaveChanges(); 

        var initialSet = new Set
        {
            MatchId = match.Id,
            SetNumber = 1,
            TeamAScore = 0,
            TeamBScore = 0,
            // WinnerTeamId = null
        };

        _dbContext.Sets.Add(initialSet);
        _dbContext.SaveChanges();

        return match;
    }
}