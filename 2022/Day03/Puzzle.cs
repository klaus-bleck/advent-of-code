using AdventOfCode.Utils;

namespace AdventOfCode.Day03;

internal class Puzzle : IPuzzle
{
    private static IEnumerable<string> GetInput() => InputProvider.Iterate(3);

    private static long GetPriority(char item) =>
        item switch
        {
            >= 'a' and <= 'z' => (int)item - 96,
            >= 'A' and <= 'Z' => (int)item - 38,
            _ => throw new NotSupportedException($"Item {item} is not supported."),
        };

    private static char GetDuplicate(string line)
    {
        var compartments = line.Chunk(line.Length / 2).ToArray();
        return compartments[0].First(x => compartments[1].Contains(x));
    }

    private static char GetDuplicate(string[] lines) => 
        lines[0].First(x => lines[1].Contains(x) && lines[2].Contains(x));

    public object SolveFirst() => GetInput().Select(GetDuplicate).Sum(x => GetPriority(x));
    public object SolveSecond() => GetInput().Chunk(3).Select(GetDuplicate).Sum(x => GetPriority(x));
}
