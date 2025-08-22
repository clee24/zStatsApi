using System.ComponentModel.DataAnnotations;

namespace zStatsApi.Dtos;

public record SetDto(
    [Required] int Id,
    int MatchId,
    int SetNumber,
    int TeamAScore,
    int TeamBScore,
    int? WinnerTeamId
);