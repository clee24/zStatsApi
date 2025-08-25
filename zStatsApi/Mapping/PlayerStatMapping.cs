using zStatsApi.Dtos.PlayerStats;
using zStatsApi.Entities;

namespace zStatsApi.Mapping;

public static class PlayerStatMapping
{
    public static PlayerStat ToEntity(this CreatePlayerStatDto stat)
    {
        return new PlayerStat
        {
            PlayerId = stat.PlayerId,
            TeamId = stat.TeamId,
            SetId = stat.SetId,
            HittingKills = stat.HittingKills,
            HittingErrors = stat.HittingErrors,
            HittingAttempts = stat.HittingAttempts,
            ServiceAces = stat.ServiceAces,
            ServiceErrors = stat.ServiceErrors,
            ServiceAttempts = stat.ServiceAttempts,
            SettingDimes = stat.SettingDimes,
            SettingErrors = stat.SettingErrors,
            SettingAttempts = stat.SettingAttempts,
            Blocks = stat.Blocks,
            Digs = stat.Digs,
            Shanks = stat.Shanks
        };
    }
    
    public static PlayerStat ToEntity(this UpdatePlayerStatDto stat, int id, int playerId, int teamId, int setId)
    {
        return new PlayerStat
        {
            Id = id,
            PlayerId = playerId,
            TeamId = teamId,
            SetId = setId,
            HittingKills = stat.HittingKills,
            HittingErrors = stat.HittingErrors,
            HittingAttempts = stat.HittingAttempts,
            ServiceAces = stat.ServiceAces,
            ServiceErrors = stat.ServiceErrors,
            ServiceAttempts = stat.ServiceAttempts,
            SettingDimes = stat.SettingDimes,
            SettingErrors = stat.SettingErrors,
            SettingAttempts = stat.SettingAttempts,
            Blocks = stat.Blocks,
            Digs = stat.Digs,
            Shanks = stat.Shanks
        };
    }

    public static PlayerStatDto ToDto(this PlayerStat stat)
    {
        return new PlayerStatDto(
            stat.Id,
            stat.PlayerId,
            stat.TeamId,
            stat.SetId,
            stat.HittingKills,
            stat.HittingErrors,
            stat.HittingAttempts,
            stat.ServiceAces,
            stat.ServiceErrors,
            stat.ServiceAttempts,
            stat.SettingDimes,
            stat.SettingErrors,
            stat.SettingAttempts,
            stat.Blocks,
            stat.Digs,
            stat.Shanks
        );
    }
}