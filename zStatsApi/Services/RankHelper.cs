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

        int lower = (int)Math.Floor(avg);
        int upper = (int)Math.Ceiling(avg);

        if (lower == upper)
            return RankMap[lower];

        return $"{RankMap[lower]}/{RankMap[upper]}";
    }
}
