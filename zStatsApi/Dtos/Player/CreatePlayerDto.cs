using System.ComponentModel.DataAnnotations;

namespace zStatsApi.Dtos.Player;

public record CreatePlayerDto (
    [Required] string Name,
    int Rank
    );