using AdventOfCode.Utils;

namespace AdventOfCode.Day08;

internal class Puzzle : IPuzzle
{
    private static IEnumerable<string> GetInput() => InputProvider.Iterate(8);

    public object SolveFirst()
    {
        var grid = Grid.Parse(GetInput());
        return grid.CalculateVisibleTrees();        
    }

    public object SolveSecond()
    {
        var grid = Grid.Parse(GetInput());
        return grid.CalculateHighestScore();
    }
}
