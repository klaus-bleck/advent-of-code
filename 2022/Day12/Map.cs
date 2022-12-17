namespace AdventOfCode.Day12;

internal sealed class Map
{
    private readonly Square[][] _squares;
    private readonly Square[] _starts;
    private readonly Square _end;

    private Map(Square[][] squares, Square[] starts, Square end)
    {
        _squares = squares;
        _starts = starts;
        _end = end;
    }

    private static Square ParseSquare(char sign, int x, int y, bool acceptImplicitStart)
    {
        if (sign == 'S' || (acceptImplicitStart && sign == 'a'))
        {
            return new('a', SquareType.Start, x, y);
        }
        else if (sign == 'E')
        {
            return new('z', SquareType.End, x, y);
        }
        return new(sign, SquareType.Default, x, y);
    }

    private static bool CanBeVisited(Square from, Square to)
    {
        if (from.Elevation < to.Elevation)
        {
            return (to.Elevation - from.Elevation) < 2;
        }
        return true;
    }

    private Square[] GetNeighbors(Square square)
    {
        var x = square.X;
        var y = square.Y;
        var count = 0;

        Square? left = null;
        Square? right = null;
        Square? top = null;
        Square? bottom = null;

        var index = y - 1;
        if (index >= 0)
        {
            top = _squares[index][x];
            count++;
        }

        index = y + 1;
        if (index < _squares.Length)
        {
            bottom = _squares[index][x];
            count++;
        }

        index = x - 1;
        if (index >= 0)
        {
            left = _squares[y][index];
            count++;
        }

        index = x + 1;
        if (index < _squares[y].Length)
        {
            right = _squares[y][index];
            count++;
        }
        index = 0;

        var neighbors = new Square[count];
        if (left.HasValue)
        {
            neighbors[index++] = left.Value;
        }
        if (right.HasValue)
        {
            neighbors[index++] = right.Value;
        }
        if (top.HasValue)
        {
            neighbors[index++] = top.Value;
        }
        if (bottom.HasValue)
        {
            neighbors[index] = bottom.Value;
        }
        return neighbors;
    }

    public static Map Parse(IEnumerable<string> input, bool acceptImplicitStart)
    {
        var squares = new List<Square[]>();
        var starts = new List<Square>();
        Square? end = null;
        int x = 0;
        int y = 0;

        foreach (var line in input)
        {
            var row = new List<Square>();
            foreach (var sign in line)
            {
                var square = ParseSquare(sign, x, y, acceptImplicitStart);
                row.Add(square);
                if (square.Type == SquareType.Start)
                {
                    starts.Add(square);
                }
                else if (square.Type == SquareType.End)
                {
                    end = square;
                }
                x++;
            }
            squares.Add(row.ToArray());

            x = 0;
            y++;
        }

        return new(
            squares.ToArray(),
            starts.Any() ? starts.ToArray() : throw new InvalidOperationException("Input does not contain at least one start square."),
            end ?? throw new InvalidOperationException("Input does not contain an end square.")
        );
    }

    public int GetShortestPath()
    {
        var visited = new HashSet<Square>();
        var queue = new Queue<Square>();
        var dist = int.MaxValue;       

        // Alternative: just count the dist in the square when visiting like next.Distance++;
        var added = 0;
        var left = _starts.Length;
        var curDist = 0;

        foreach(var start in _starts)
        {
            visited.Add(start);
            queue.Enqueue(start);
        }

        while (queue.Count > 0)
        {
            var next = queue.Dequeue();
            if (next == _end)
            {
                return curDist;
            }

            var neighbors = GetNeighbors(next);
            foreach (var neighbor in neighbors)
            {
                if (!visited.Contains(neighbor) && CanBeVisited(next, neighbor))
                {
                    visited.Add(neighbor);
                    queue.Enqueue(neighbor);
                    added++;
                }
            }

            left--;
            if (left == 0)
            {
                left = added;
                added = 0;
                curDist++;
            }
        }
        return dist;
    }
}
