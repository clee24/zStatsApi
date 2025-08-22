using Microsoft.EntityFrameworkCore;
using zStatsApi.Entities;

namespace zStatsApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Match> Matches { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<PlayerStat> PlayerStats { get; set; }
    public DbSet<Set> Sets { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamPlayer> TeamPlayers { get; set; }
}