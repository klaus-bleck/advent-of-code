namespace AdventOfCode.Day02;

internal class Puzzle : PuzzleBase
{
    private const char GameSeparator = ':';
    private const char SubsetSeparator = ';';
    private const char ColorSeparator = ',';
    private const char SpaceSeparator = ' ';
    private static readonly Dictionary<string, int> _possibleColorCounts = new()
    {
        ["red"] = 12,
        ["green"] = 13,
        ["blue"] = 14,
    };

    protected override uint Day => 2;

    private static (int GameIndex, Dictionary<string, int[]> SubsetColorCounts) ParseLine(string line, string[] possibleColors)
    {
        var span = line.AsSpan();

        var gameStringIndex = span.IndexOf(GameSeparator);
        var game = span[0..gameStringIndex];
        var gameSpaceIndex = span.IndexOf(SpaceSeparator);
        var gameIndex = int.Parse(game[gameSpaceIndex..^0]);
        span = span[(gameStringIndex + 1)..^0];

        // Split on a span does only work with a known size,
        // so let's assume there aren't more than 10 subsets in a game...
        // Otherwise we have to use a string with allocation
        Span<Range> subsets = new(new Range[10]);
        var subsetCount = span.Split(subsets, SubsetSeparator, StringSplitOptions.TrimEntries);
        ReadOnlySpan<char> currentSubset;

        Span<Range> colors = new(new Range[possibleColors.Length]);
        ReadOnlySpan<char> currentColor;
        Dictionary<string, int[]> colorData = possibleColors.ToDictionary(x => x, x => new int[subsetCount]);

        for (int s = 0; s < subsetCount; s++)
        {
            currentSubset = span[subsets[s].Start..subsets[s].End];
            var colorCount = currentSubset.Split(colors, ColorSeparator, StringSplitOptions.TrimEntries);
            for (int c = 0; c < colorCount; c++)
            {
                currentColor = currentSubset[colors[c].Start..colors[c].End];
                var colorSpaceIndex = currentColor.IndexOf(SpaceSeparator);
                var number = int.Parse(currentColor[0..colorSpaceIndex]);
                var color = currentColor[(colorSpaceIndex + 1)..^0];
                colorData[color.ToString()][s] = number;
            }
        }
        return (gameIndex, colorData);
    }

    private static int GetProductOfPossibleGame(string line)
    {
        (var _, var subsetColorCounts) = ParseLine(line, [.. _possibleColorCounts.Keys]);
        return _possibleColorCounts.Keys.Aggregate(1, (accumulator, next) => 
            subsetColorCounts[next].Max() * accumulator);
    }

    private static int GetPossibleGameIndex(string line)
    {
        (var gameIndex, var subsetColorCounts) = ParseLine(line, [.. _possibleColorCounts.Keys]);
        foreach (var colorCounts in subsetColorCounts)
        {
            foreach (var colorCount in colorCounts.Value)
            {
                if (colorCount > _possibleColorCounts[colorCounts.Key])
                {
                    return 0;
                }
            }
        }
        return gameIndex;
    }

    public override object SolveFirst() => GetInput().Sum(GetPossibleGameIndex);

    public override object SolveSecond() => GetInput().Sum(GetProductOfPossibleGame);
}
