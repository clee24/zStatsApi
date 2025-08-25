using System.ComponentModel.DataAnnotations;

namespace zStatsApi.Dtos.PlayerStats;

public record CreatePlayerStatDto(
    [Required] int PlayerId,
    [Required] int TeamId,
    [Required] int SetId,
    int HittingKills,
    int HittingErrors,
    int HittingAttempts,
    int ServiceAces,
    int ServiceErrors,
    int ServiceAttempts,
    int SettingDimes,
    int SettingErrors,
    int SettingAttempts,
    int Blocks,
    int Digs,
    int Shanks
);