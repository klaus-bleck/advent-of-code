namespace AdventOfCode.Day08;

public class Puzzle : PuzzleBase
{
    protected override uint Day => 8;

    public override object SolveFirst()
    {
        var grid = Grid.Parse(GetInput());
        return grid.CalculateVisibleTrees();        
    }

    public override object SolveSecond()
    {
        var grid = Grid.Parse(GetInput());
        return grid.CalculateHighestScore();
    }
}
