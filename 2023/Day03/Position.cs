namespace AdventOfCode.Day03;

public record struct Position(int Row, int Column)
{
    public readonly IEnumerable<Position> GetAdjacents()
    {
        yield return new(Row, Column + 1);
        yield return new(Row + 1, Column + 1);
        yield return new(Row + 1, Column);
        yield return new(Row + 1, Column - 1);
        yield return new(Row, Column - 1);
        yield return new(Row - 1, Column - 1);
        yield return new(Row - 1, Column);
        yield return new(Row - 1, Column + 1);
    }
}
