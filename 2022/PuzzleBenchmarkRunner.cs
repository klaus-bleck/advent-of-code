using BenchmarkDotNet.Attributes;

namespace AdventOfCode;

public class PuzzleBenchmarkRunner<TPuzzle>
    where TPuzzle : PuzzleBase, new()
{
    private readonly TPuzzle _puzzle;

    public PuzzleBenchmarkRunner()
    {
        _puzzle = new();
    }

    [Benchmark]
    public object RunFirst() => _puzzle.SolveFirst();

    [Benchmark]
    public object RunSecond() => _puzzle.SolveSecond();
}
