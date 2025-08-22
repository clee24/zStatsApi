namespace zStatsApi.Dtos.Player;

public record UpdatePlayerDto (
    string FullName,
    string? Nickname
);