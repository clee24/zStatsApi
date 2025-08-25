using zStatsApi.Dtos.Set;
using zStatsApi.Entities;

namespace zStatsApi.Mapping;

public static class SetMapping
{
    public static Set ToEntity(this CreateSetDto set)
    {
        return new Set
        {
            MatchId = set.MatchId,
            SetNumber = set.SetNumber,
            TeamAScore = set.TeamAScore,
            TeamBScore = set.TeamBScore
        };
    }
    
    public static Set ToEntity(this UpdateSetDto set, int id)
    {
        return new Set
        {
            Id = id,
            SetNumber = set.SetNumber,
            TeamAScore = set.TeamAScore,
            TeamBScore = set.TeamBScore,
            WinnerTeamId = set.WinnerTeamId
        };
    }

    public static SetDto ToDto(this Set set)
    {
        return new SetDto(
            set.Id,
            set.MatchId,
            set.SetNumber,
            set.TeamAScore,
            set.TeamBScore,
            set.WinnerTeamId
        );
    }
}