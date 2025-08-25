using zStatsApi.Dtos.Player;
using zStatsApi.Entities;

namespace zStatsApi.Mapping;

public static class PlayerMapping
{
    public static Player ToEntity(this CreatePlayerDto player)
    {
        return new Player
        {
            Name = player.FullName
        };
    }
    
    public static Player ToEntity(this UpdatePlayerDto player, int id)
    {
        return new Player
        {
            Id = id,
            Name = player.FullName,
        };
    }

    public static PlayerDto ToDto(this Player player)
    {
        return new PlayerDto(
            player.Id,
            player.Name,
            player.Nickname
        );
    }
}