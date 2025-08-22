using System.ComponentModel.DataAnnotations;

namespace zStatsApi.Dtos.Player;

public record CreatePlayerDto (
    [Required] string FullName,
    string? Nickname
    );