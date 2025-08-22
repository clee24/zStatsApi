using System.ComponentModel.DataAnnotations;

namespace zStatsApi.Dtos.Player;

public record PlayerDto(
    [Required] int Id,
    [Required] string FullName,
    string? Nickname = null
);