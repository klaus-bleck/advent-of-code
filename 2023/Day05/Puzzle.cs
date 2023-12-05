namespace AdventOfCode.Day05;

public class Puzzle : PuzzleBase
{
    protected override uint Day => 5;

    private static Almanac Parse(IEnumerable<string> lines)
    {
        List<Map> maps = [];
        List<Mapping> currentMappings = [];
        string? currentMapName = null;

        var firstLine = lines.First();
        var indexOfColon = firstLine.IndexOf(':');
        long[] seeds = firstLine[(indexOfColon + 1)..^0]
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse).ToArray();

        void AddCurrentToMaps()
        {
            if (currentMapName != null)
            {
                maps.Add(new(currentMapName, [.. currentMappings]));
                currentMappings.Clear();
            }
        }

        foreach (var line in lines.Skip(1).Where(x => x.Length > 0))
        {
            indexOfColon = line.IndexOf(':');
            if (indexOfColon > 0)
            {
                AddCurrentToMaps();
                currentMapName = line[..^5];
            }
            else
            {
                var mappingValues = line.Split(" ").Select(long.Parse).ToArray();
                currentMappings.Add(new(mappingValues[1], mappingValues[2], mappingValues[0]));
            }
        }

        AddCurrentToMaps();
        return new(seeds, [.. maps]);
    }

    public override object SolveFirst() => Parse(GetInput()).GetLowestLocation();

    public override object SolveSecond() => Parse(GetInput()).GetLowestLocationWithPairs();
}
