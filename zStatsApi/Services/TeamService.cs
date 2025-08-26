using Microsoft.EntityFrameworkCore;
using zStatsApi.Data;
using zStatsApi.Dtos.Team;
using zStatsApi.Entities;

namespace zStatsApi.Services;

public class TeamService
{
    private readonly ZStatsContext _dbContext;

    public TeamService(ZStatsContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Team> CreateTeamAsync(CreateTeamDto dto)
    {
        var team = new Team { Name = dto.Name };

        // Add Player A if provided
        if (dto.PlayerAId.HasValue)
        {
            var playerA = await _dbContext.Players.FindAsync(dto.PlayerAId.Value);
            if (playerA != null)
                team.TeamPlayers.Add(new TeamPlayer { Player = playerA });
        }

        // Add Player B if provided
        if (dto.PlayerBId.HasValue)
        {
            var playerB = await _dbContext.Players.FindAsync(dto.PlayerBId.Value);
            if (playerB != null)
                team.TeamPlayers.Add(new TeamPlayer { Player = playerB });
        }

        _dbContext.Teams.Add(team);
        await _dbContext.SaveChangesAsync();
        return team;
    }

    public async Task UpdateTeamAsync(int id, UpdateTeamDto dto)
    {
        var team = await _dbContext.Teams
            .Include(t => t.TeamPlayers)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (team == null)
            throw new ArgumentException("Team not found.");

        team.Name = dto.Name;

        if (dto.ReplacePlayerId.HasValue)
        {
            var toRemove = team.TeamPlayers
                .Where(tp => tp.PlayerId == dto.ReplacePlayerId.Value)
                .ToList();
            foreach (var tp in toRemove)
                team.TeamPlayers.Remove(tp);
        }

        if (dto.WithPlayerId.HasValue)
        {
            var player = await _dbContext.Players.FindAsync(dto.WithPlayerId.Value);
            if (player != null)
                team.TeamPlayers.Add(new TeamPlayer { Player = player });
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteTeamAsync(int id)
    {
        var team = await _dbContext.Teams
            .Include(t => t.TeamPlayers)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (team == null)
            throw new ArgumentException("Team not found.");

        _dbContext.TeamPlayers.RemoveRange(team.TeamPlayers);
        _dbContext.Teams.Remove(team);
        await _dbContext.SaveChangesAsync();
    }
}
