using System.ComponentModel.DataAnnotations;

namespace zStatsApi.Dtos.Team;

public record CreateTeamDto (
    [Required] string Name
);