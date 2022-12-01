namespace AdventOfCode.Day01;

internal class Puzzle1 : IPuzzle
{
    public void Solve()
    {
        var lastDivider = -1;
        var nextGroupIndex = 0;

        var max = InputProvider
            .Iterate()
            .Select((x, i) =>
            {
                if (x.Length == 0)
                {
                    lastDivider = i;
                    nextGroupIndex = lastDivider + 1;
                    return new Item(i, 0);
                }
                return new Item(nextGroupIndex, int.Parse(x));
            })
            .GroupBy(x => x.Index)
            .Max(x => x.Sum(y => y.Value));

        Console.WriteLine(max);
    }
}
