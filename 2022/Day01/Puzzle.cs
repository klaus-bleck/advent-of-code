namespace AdventOfCode.Day01;

public class Puzzle : IPuzzle
{
    private static long GetCalories(int elfCount)
    {
        var lastDivider = -1;
        var nextGroupIndex = 0;

        return InputProvider
            .Iterate()
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

    public object SolveFirst() => GetCalories(1);
    public object SolveSecond() => GetCalories(3);
}