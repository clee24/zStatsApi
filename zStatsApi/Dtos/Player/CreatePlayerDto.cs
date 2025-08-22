namespace zStatsApi.Dtos.Player;

public record CreatePlayerDto (
    string FullName,
    string? Nickname
    );