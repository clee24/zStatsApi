using zStatsApi.Dtos.Match;
using zStatsApi.Entities;

namespace zStatsApi.Mapping;

public static class MatchMapping
{
    public static Match ToEntity(this CreateMatchDto match)
    {
        return new ()
        {
            Date = match.Date,
            Location = match.Location,
            TeamAId = match.TeamAId,
            TeamBId = match.TeamBId,
        };
    }
    
    public static Match ToEntity(this UpdateMatchDto match, int id)
    {
        return new ()
        {
            Id = id,
            Date = match.Date,
            Location = match.Location,
            TeamAId = match.TeamAId,
            TeamBId = match.TeamBId,
        };
    }

    public static MatchDto ToDto(this Match match)
    {
        return new(
            match.Id,
            match.Date,
            match.Location,
            match.TeamAId,
            match.TeamBId,
            match.WinnerTeamId
        );
    }
}