using AdventOfCode.Utils;

namespace AdventOfCode.Day04;

internal class Puzzle : IPuzzle
{
    private static IEnumerable<string> GetInput() => InputProvider.Iterate(4);    

    private static long[][] GetElfRanges(string line) =>
        line.Split(",").Select(x => x.Split("-").Select(long.Parse).ToArray()).ToArray();

    private static bool IsContained(long[] input, long[] range) =>
        input[0] >= range[0] && input[1] <= range[1];

    private static bool IsFullyContained(string line)
    {
        var elfRanges = GetElfRanges(line);
        return IsContained(elfRanges[0], elfRanges[1]) || IsContained(elfRanges[1], elfRanges[0]);
    }

    private static bool IsOverlapping(long[] input, long[] range) =>
        (input[0] >= range[0] && input[0] <= range[1]) || (input[1] >= range[0] && input[1] <= range[1]);

    private static bool IsOverlapping(string line)
    {
        var elfRanges = GetElfRanges(line);
        return IsOverlapping(elfRanges[0], elfRanges[1]) || IsOverlapping(elfRanges[1], elfRanges[0]);
    }

    public object SolveFirst() => GetInput().Count(IsFullyContained);
    public object SolveSecond() => GetInput().Count(IsOverlapping);
}
