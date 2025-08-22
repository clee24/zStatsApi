namespace zStatsApi.Entities;

public class Set
{
    public int Id { get; set; }
    public int MatchId { get; set; }
    public Match Match { get; set; } = null!;

    public int SetNumber { get; set; }
    public int TeamAScore { get; set; }
    public int TeamBScore { get; set; }

    public int? WinnerTeamId { get; set; }
    public Team? WinnerTeam { get; set; }

    public ICollection<PlayerStat> PlayerStats { get; set; } = new List<PlayerStat>();
}