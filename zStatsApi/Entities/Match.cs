namespace zStatsApi.Entities;

public class Match
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; } = null!;

    public int TeamAId { get; set; }
    public Team TeamA { get; set; } = null!;

    public int TeamBId { get; set; }
    public Team TeamB { get; set; } = null!;

    public int? WinnerTeamId { get; set; }
    public Team? WinnerTeam { get; set; }

    public ICollection<Set> Sets { get; set; } = new List<Set>();
}