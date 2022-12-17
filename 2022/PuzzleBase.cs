using AdventOfCode.Utils;

namespace AdventOfCode;

public abstract class PuzzleBase : IPuzzle
{
    protected abstract uint Day { get; }

    protected IEnumerable<string> GetInput() => InputProvider.Iterate(Day);
    protected IEnumerable<string> GetSample() => InputProvider.IterateSample(Day);

    public abstract object SolveFirst();

    public abstract object SolveSecond();
}
