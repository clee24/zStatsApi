namespace zStatsApi.Dtos;

public record PlayerDto(
    int Id,
    string FullName,
    string? Nickname = null
);