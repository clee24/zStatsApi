using System.ComponentModel.DataAnnotations;

namespace zStatsApi.Dtos.Team;

public record TeamDto (
    [Required] int Id,
    [Required] string Name
    );