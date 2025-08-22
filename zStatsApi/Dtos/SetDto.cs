namespace zStatsApi.Dtos;

public record SetDto
{
    public int Id { get; set; }
    public int SetNumber { get; set; }
    public int TeamAScore { get; set; }
    public int TeamBScore { get; set; }
    public List<PlayerStatDto> PlayerStats { get; set; } = new();
}