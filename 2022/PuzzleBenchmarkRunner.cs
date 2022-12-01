using BenchmarkDotNet.Attributes;

namespace AdventOfCode;

public class PuzzleBenchmarkRunner<TPuzzle>
    where TPuzzle : IPuzzle, new()
{
    private readonly TPuzzle _puzzle;

    public PuzzleBenchmarkRunner()
    {
        _puzzle = new();
    }

    [Benchmark]
    public void RunFirst() => _puzzle.SolveFirst();

    [Benchmark]
    public void RunSecond() => _puzzle.SolveSecond();
}
