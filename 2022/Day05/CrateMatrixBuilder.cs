namespace AdventOfCode.Day05;

internal sealed class CrateMatrixBuilder
{
    private readonly List<string> _source;

    public CrateMatrixBuilder()
    {
        _source = new List<string>();
    }

    public void Add(string line) => _source.Add(line);

    public CrateMatrix Build()
    {
        var matrix = new CrateMatrix(_source.Take(_source.Count - 1).ToArray());
        _source.Clear();
        return matrix;
    }
}
