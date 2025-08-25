using zStatsApi.Dtos.Team;
using zStatsApi.Entities;

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
        return new TeamDto(
            team.Id,
            team.Name
        );
    }
}