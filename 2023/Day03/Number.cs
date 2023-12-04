using System.Collections;

namespace AdventOfCode.Day03;

public record Number(int Value, int Row, int ColumnStart, int ColumnEnd) : IEnumerable<Position>
{
    public IEnumerator<Position> GetEnumerator()
    {
        for (int column = ColumnStart; column <= ColumnEnd; column++)
        {
            yield return new Position(Row, column);
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
