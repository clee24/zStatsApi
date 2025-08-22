namespace zStatsApi.Dtos;

public record CreateSetDto(
    int MatchId,
    int SetNumber,
    int TeamAScore,
    int TeamBScore
);