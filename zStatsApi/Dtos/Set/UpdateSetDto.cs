using System.ComponentModel.DataAnnotations;

namespace zStatsApi.Dtos.Set;

public record UpdateSetDto(
    [Required] int SetNumber,
    [Required] int TeamAScore,
    [Required] int TeamBScore,
    int? WinnerTeamId
);