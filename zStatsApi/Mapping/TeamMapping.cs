using zStatsApi.Dtos.Team;
using zStatsApi.Entities;
using zStatsApi.Services;

namespace zStatsApi.Mapping;

public static class TeamMapping
{
    public static Team ToEntity(this CreateTeamDto team)
    {
        return new Team
        {
            Name = team.Name
        };
    }
    
    public static Team ToEntity(this UpdateTeamDto team, int id)
    {
        return new Team
        {
            Id = id,
            Name = team.Name
        };
    }

    public static TeamDto ToDto(this Team team)
    {
        var playerRanks = team.TeamPlayers
            .Select(tp => tp.Player.Rank)
            .ToList();

        var rankLabel = RankHelper.GetTeamRankLabel(playerRanks);

        return new TeamDto(
            team.Id,
            team.Name,
            rankLabel
        );
    }

}