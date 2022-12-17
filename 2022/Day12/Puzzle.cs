namespace AdventOfCode.Day12;

public class Puzzle : PuzzleBase
{
    protected override uint Day => 12;

    public override object SolveFirst()
    {
        var map = Map.Parse(GetInput(), false);
        return map.GetShortestPath();
    }

    public override object SolveSecond()
    {
        var map = Map.Parse(GetInput(), true);
        return map.GetShortestPath();
    }
}
