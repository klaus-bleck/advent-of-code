using AdventOfCode.Utils;

namespace AdventOfCode;

public abstract class PuzzleBase : IPuzzle
{
    protected abstract uint Day { get; }

    protected IEnumerable<string> GetInput() => InputProvider.Iterate(Day);
    protected IEnumerable<string> GetSample() => InputProvider.IterateSample(Day);

    public virtual object SolveFirst() => string.Empty;

    public virtual object SolveSecond() => string.Empty;
}
