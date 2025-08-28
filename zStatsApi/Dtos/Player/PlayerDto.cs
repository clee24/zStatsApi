using System.ComponentModel.DataAnnotations;

namespace zStatsApi.Dtos.Player;

public record PlayerDto(
    int Id,
    string Name,
    int Rank
);