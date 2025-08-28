namespace zStatsApi.Services;

public static class RankHelper
{
    private static readonly Dictionary<int, string> RankMap = new()
    {
        {1, "AA"},
        {2, "A"},
        {3, "BB"},
        {4, "B"},
        {5, "C"}
    };

    public static string GetTeamRankLabel(IEnumerable<int> playerRanks)
    {
        var avg = playerRanks.Average();
        var lower = (int)Math.Floor(avg);
        var upper = (int)Math.Ceiling(avg);

        string lowerLabel = RankMap.GetValueOrDefault(lower, $"Rank {lower}");
        string upperLabel = RankMap.GetValueOrDefault(upper, $"Rank {upper}");

        return lower == upper ? lowerLabel : $"{lowerLabel}/{upperLabel}";
    }
}
