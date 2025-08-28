using System.ComponentModel.DataAnnotations;

namespace zStatsApi.Dtos.Team;

public record TeamDto (
    int Id,
    string Name,
    string RankLabel
    );