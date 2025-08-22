namespace zStatsApi.Entities;

public class TeamPlayer
{
    public int TeamId { get; set; }
    public Team Team { get; set; } = null!;
    
    public int PlayerId { get; set; }
    public Player Player { get; set; } = null!;
}