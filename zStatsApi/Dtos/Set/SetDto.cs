namespace zStatsApi.Dtos.Set;

public record SetDto(
    int Id,
    int MatchId,
    int SetNumber,
    int TeamAScore,
    int TeamBScore,
    int? WinnerTeamId
);