namespace zStatsApi.Entities;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<TeamPlayer> TeamPlayers { get; set; } = new List<TeamPlayer>();
    public ICollection<Match> MatchesAsTeamA { get; set; } = new List<Match>();
    public ICollection<Match> MatchesAsTeamB { get; set; } = new List<Match>();
}