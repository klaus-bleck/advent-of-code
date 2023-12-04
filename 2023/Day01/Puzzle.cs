namespace AdventOfCode.Day01;

public class Puzzle : PuzzleBase
{
    private static readonly string[] _numbers =
    [
        "1",
        "2",
        "3",
        "4",
        "5",
        "6",
        "7",
        "8",
        "9",
    ];

    private static readonly string[] _words =
    [
        "one",
        "two",
        "three",
        "four",
        "five",
        "six",
        "seven",
        "eight",
        "nine",
    ];

    private static readonly string[] _all = _numbers.Union(_words).ToArray();

    protected override uint Day => 1;

    private static int ParseLines(string line, string[] representation)
    {
        var span = line.AsSpan();
        List<(int Index, int Number)> matches = [];

        for (int i = 0; i < representation.Length; i++)
        {
            var current = representation[i];
            int[] indices = [span.IndexOf(current), span.LastIndexOf(current)];
            matches.AddRange(indices.Where(x => x >= 0).Select(x => (x, (i % 9) + 1)));
        }

        var orderedMatches = matches.OrderBy(x => x.Index);
        return orderedMatches.First().Number * 10 + orderedMatches.Last().Number;
    }

    public override object SolveFirst() => GetInput().Sum(x => ParseLines(x, _numbers));

    public override object SolveSecond() => GetInput().Sum(x => ParseLines(x, _all));
}
