namespace zStatsApi.Dtos.PlayerStats;
public record PlayerStatDto(
    int Id,
    int PlayerId,
    int SetId,
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