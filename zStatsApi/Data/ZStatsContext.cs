using Microsoft.EntityFrameworkCore;
using zStatsApi.Entities;

namespace zStatsApi.Data;

public class ZStatsContext(DbContextOptions<ZStatsContext> options) : DbContext(options)
{
    public DbSet<Player> Players => Set<Player>();
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<Match> Matches => Set<Match>();
    public DbSet<Set> Sets => Set<Set>();
    public DbSet<TeamPlayer> TeamPlayers => Set<TeamPlayer>();
    public DbSet<PlayerStat> PlayerStats => Set<PlayerStat>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        // Relationships
        modelBuilder.Entity<Match>()
            .HasOne(m => m.TeamA)
            .WithMany(t => t.MatchesAsTeamA)
            .HasForeignKey(m => m.TeamAId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Match>()
            .HasOne(m => m.TeamB)
            .WithMany(t => t.MatchesAsTeamB)
            .HasForeignKey(m => m.TeamBId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Set>()
            .HasOne(s => s.WinnerTeam)
            .WithMany()
            .HasForeignKey(s => s.WinnerTeamId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Match>()
            .HasOne(m => m.WinnerTeam)
            .WithMany()
            .HasForeignKey(m => m.WinnerTeamId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TeamPlayer>()
            .HasKey(tp => new { tp.TeamId, tp.PlayerId });

        modelBuilder.Entity<TeamPlayer>()
            .HasOne(tp => tp.Team)
            .WithMany(t => t.TeamPlayers)
            .HasForeignKey(tp => tp.TeamId);

        modelBuilder.Entity<TeamPlayer>()
            .HasOne(tp => tp.Player)
            .WithMany(p => p.TeamPlayers)
            .HasForeignKey(tp => tp.PlayerId);

        //  Seed Data

        // Teams
        modelBuilder.Entity<Team>().HasData(
            new Team { Id = 1, Name = "The Holy Trinity" },
            new Team { Id = 2, Name = "Double Trouble" }
        );

        // Players
        modelBuilder.Entity<Player>().HasData(
            new Player { Id = 1, Name = "Chris", Rank = 3},
            new Player { Id = 2, Name = "Trinity", Rank = 2},
            new Player { Id = 3, Name = "Zey", Rank = 1},
            new Player { Id = 4, Name = "Jimmy", Rank = 1}
        );

        // TeamPlayers (Many-to-many)
        modelBuilder.Entity<TeamPlayer>().HasData(
            new TeamPlayer { TeamId = 1, PlayerId = 1 },
            new TeamPlayer { TeamId = 1, PlayerId = 2 },
            new TeamPlayer { TeamId = 2, PlayerId = 3 },
            new TeamPlayer { TeamId = 2, PlayerId = 4 }
        );

        // Matches
        modelBuilder.Entity<Match>().HasData(
            new Match
            {
                Id = 1,
                Date = new DateTime(2025, 01, 15, 0, 0, 0, DateTimeKind.Utc),
                Location = "Whiskey Island",
                TeamAId = 1,
                TeamBId = 2,
                WinnerTeamId = null
            },
            new Match
            {
                Id = 2,
                Date = new DateTime(2025, 01, 20, 0, 0, 0, DateTimeKind.Utc),
                Location = "Mulberry's",
                TeamAId = 2,
                TeamBId = 1,
                WinnerTeamId = 2
            }
        );

        // Sets
        modelBuilder.Entity<Set>().HasData(
            new Set { Id = 1, MatchId = 1, SetNumber = 1, TeamAScore = 25, TeamBScore = 23, WinnerTeamId = 1 },
            new Set { Id = 2, MatchId = 1, SetNumber = 2, TeamAScore = 21, TeamBScore = 25, WinnerTeamId = 2 },
            new Set { Id = 3, MatchId = 1, SetNumber = 3, TeamAScore = 15, TeamBScore = 4, WinnerTeamId = 1 }
        );

        // PlayerStats
        modelBuilder.Entity<PlayerStat>().HasData(
            new PlayerStat
            {
                Id = 1,
                PlayerId = 1,
                SetId = 1,
                TeamId = 1,
                HittingKills = 5,
                HittingErrors = 2,
                HittingAttempts = 10,
                ServiceAces = 2,
                ServiceErrors = 1,
                ServiceAttempts = 8,
                SettingDimes = 3,
                SettingErrors = 0,
                SettingAttempts = 15,
                Blocks = 1,
                Digs = 4,
                Shanks = 1
            },
            new PlayerStat
            {
                Id = 2,
                PlayerId = 2,
                SetId = 1,
                TeamId = 1,
                HittingKills = 3,
                HittingErrors = 1,
                HittingAttempts = 8,
                ServiceAces = 1,
                ServiceErrors = 2,
                ServiceAttempts = 6,
                SettingDimes = 2,
                SettingErrors = 1,
                SettingAttempts = 12,
                Blocks = 0,
                Digs = 5,
                Shanks = 0
            }
        );
    }
}