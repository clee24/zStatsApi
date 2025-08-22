namespace zStatsApi.Dtos.Player;

public record PlayerDto(
    int Id,
    string FullName,
    string? Nickname = null
);