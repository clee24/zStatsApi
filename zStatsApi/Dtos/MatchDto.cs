namespace zStatsApi.Dtos;

public record MatchDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; } = null!;
    public TeamDto TeamA { get; set; } = null!;
    public TeamDto TeamB { get; set; } = null!;
    public int? WinnerTeamId { get; set; }
    public List<SetDto> Sets { get; set; } = new();
}