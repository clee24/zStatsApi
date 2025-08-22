using System.ComponentModel.DataAnnotations;

namespace zStatsApi.Dtos.Match;

public record CreateMatchDto(
    [Required] DateTime Date,
    string Location,
    [Required] int TeamAId,
    [Required]int TeamBId
);
