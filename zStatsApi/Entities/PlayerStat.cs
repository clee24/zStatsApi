namespace zStatsApi.Entities;

public class PlayerStat
{
    public int Id { get; set; }

    public int PlayerId { get; set; }
    public Player Player { get; set; } = null!;

    public int SetId { get; set; }
    public Set Set { get; set; } = null!;

    public int HittingKills { get; set; }
    public int HittingErrors { get; set; }
    public int HittingAttempts { get; set; }

    public int ServiceAces { get; set; }
    public int ServiceErrors { get; set; }
    public int ServiceAttempts { get; set; }

    public int SettingDimes { get; set; }
    public int SettingErrors { get; set; }
    public int SettingAttempts { get; set; }

    public int Blocks { get; set; }
    public int Digs { get; set; }
    public int Shanks { get; set; }
}