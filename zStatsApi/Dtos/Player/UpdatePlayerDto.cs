using System.ComponentModel.DataAnnotations;

namespace zStatsApi.Dtos.Player;

public record UpdatePlayerDto (
    [Required] string FullName,
    string? Nickname
);