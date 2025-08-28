using System.ComponentModel.DataAnnotations;

namespace zStatsApi.Dtos.Match;

public record CreateMatchDto(
    DateTime Date,
    string Location,
    int TeamAId,
    int TeamBId
);
