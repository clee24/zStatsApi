namespace zStatsApi.Dtos;

public record UpdateSetDto(
    int SetNumber,
    int TeamAScore,
    int TeamBScore,
    int? WinnerTeamId
);