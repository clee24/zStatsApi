using System.ComponentModel.DataAnnotations;
namespace zStatsApi.Dtos;

public record CreatePlayerDto (
    string FullName,
    string? Nickname
    );