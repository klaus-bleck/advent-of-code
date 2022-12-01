namespace AdventOfCode;

internal static class PuzzleRunner
{    
    public static void Run<TPuzzle>() where TPuzzle : IPuzzle, new()
    {
        var puzzle = new TPuzzle();
        Console.WriteLine(puzzle.SolveFirst());
        Console.WriteLine(puzzle.SolveSecond());
    }
}
