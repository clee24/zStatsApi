namespace zStatsApi.Dtos.PlayerStats;

public record UpdatePlayerStatsDto(
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