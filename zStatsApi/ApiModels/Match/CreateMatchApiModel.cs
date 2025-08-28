using System.ComponentModel.DataAnnotations;

namespace zStatsApi.ApiModels.Match;

public record CreateMatchApiModel (
    [Required] DateTime Date,
    string Location,
    [Required] int TeamAId,
    [Required] int TeamBId
);