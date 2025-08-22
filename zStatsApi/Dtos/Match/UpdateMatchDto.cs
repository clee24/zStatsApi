using System.ComponentModel.DataAnnotations;

namespace zStatsApi.Dtos.Match;

public record UpdateMatchDto(
    [Required] DateTime Date,
    string Location,
    [Required] int TeamAId,
    [Required] int TeamBId,
    int? WinnerTeamId
);