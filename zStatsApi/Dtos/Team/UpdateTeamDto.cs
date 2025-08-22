using System.ComponentModel.DataAnnotations;

namespace zStatsApi.Dtos.Team;

public record UpdateTeamDto (
    [Required] string Name
);