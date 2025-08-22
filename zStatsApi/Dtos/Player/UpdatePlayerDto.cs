namespace zStatsApi.Dtos;

public record UpdatePlayerDto (
    string FullName,
    string? Nickname
);