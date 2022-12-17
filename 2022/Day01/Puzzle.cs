namespace AdventOfCode.Day01;

public class Puzzle : PuzzleBase
{
    protected override uint Day => 1;

    private long GetCalories(int elfCount)
    {
        var lastDivider = -1;
        var nextGroupIndex = 0;

        return GetInput()
            .Select((x, i) =>
            {
                if (x.Length == 0)
                {
                    lastDivider = i;
                    nextGroupIndex = lastDivider + 1;
                    return new Item(i, 0);
                }
                return new Item(nextGroupIndex, long.Parse(x));
            })
            .GroupBy(x => x.Index)
            .ToDictionary(x => x.Key, x => x.Sum(y => y.Value))
            .OrderByDescending(x => x.Value)
            .Take(elfCount)
            .Sum(x => x.Value);
    }

    public override object SolveFirst() => GetCalories(1);
    public override object SolveSecond() => GetCalories(3);
}