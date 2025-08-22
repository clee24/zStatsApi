namespace zStatsApi.Dtos.Match;

public record MatchDto(
    int Id,
    DateTime Date,
    string Location,
    int TeamAId,
    int TeamBId,
    int? WinnerTeamId
);