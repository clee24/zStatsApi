namespace zStatsApi.Entities;

public class Player
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Nickname { get; set; }
    public string? Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }

    public ICollection<TeamPlayer> TeamPlayers { get; set; } = new List<TeamPlayer>();
    public ICollection<PlayerStat> PlayerStats { get; set; } = new List<PlayerStat>();
}