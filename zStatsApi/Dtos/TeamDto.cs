namespace zStatsApi.Dtos;

public record TeamDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<PlayerDto> Players { get; set; } = new();
}