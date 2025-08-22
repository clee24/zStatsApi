using System.ComponentModel.DataAnnotations;

namespace zStatsApi.Dtos.Set;

public record CreateSetDto(
    [Required] int MatchId,
    int SetNumber,
    int TeamAScore,
    int TeamBScore
);