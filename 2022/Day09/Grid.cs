using System.Diagnostics;

namespace AdventOfCode.Day09;

internal sealed class Grid
{
    private readonly Dictionary<int, HashSet<Marker>> _visitedPositions;
    private readonly Position[] _knots;

    public Grid()
        : this(0)
    {

    }

    public Grid(int knots)
    {
        _knots = new Position[knots];
        _visitedPositions = new Dictionary<int, HashSet<Marker>>();

        for (int i = 0; i < _knots.Length; i++)
        {
            _knots[i] = Position.Zero;
            _visitedPositions[i] = new HashSet<Marker>();
            SetMarkerForKnotAt(i);
        }
    }

    private static int Reduce(int value) =>
        value < 0 ? value + 1 : value - 1;

    private static Position? CalculateOffset(Position a, Position b)
    {
        var difference = a - b;
        var offset = new Position(
            Math.Abs(difference.X) > 1 ? Reduce(difference.X) : difference.X,
            Math.Abs(difference.Y) > 1 ? Reduce(difference.Y) : difference.Y
        );
        return difference.Equals(offset) ? null : offset;
    }

    private void SetMarkerForKnotAt(int index) =>
        _visitedPositions[index].Add(new(_knots[index].X, _knots[index].Y));

    private void MoveKnotAtIndex(int index, Position offset)
    {
        _knots[index].Move(offset);
        SetMarkerForKnotAt(index);
    }

    private void UpdateKnotAtIndex(int index)
    {
        var offset = CalculateOffset(_knots[index - 1], _knots[index]);
        if (offset is not null)
        {
            MoveKnotAtIndex(index, offset);
        }
    }

    private void Simulate(int steps, int horizontalOffset, int verticalOffset)
    {
        for (uint i = 0; i < steps; i++)
        {
            MoveKnotAtIndex(0, new(horizontalOffset, verticalOffset));
            for (int j = 1; j < _knots.Length; j++)
            {
                UpdateKnotAtIndex(j);
            }
        }
    }

    public void MoveLeft(int offset) => Simulate(offset, -1, 0);

    public void MoveRight(int offset) => Simulate(offset, 1, 0);

    public void MoveUp(int offset) => Simulate(offset, 0, 1);

    public void MoveDown(int offset) => Simulate(offset, 0, -1);

    public int GetVisitedMarkerForKnot(int number) => _visitedPositions[number - 1].Count;

    public void PrintVisitedMarkerForKnot(int number)
    {
        var visitedPositions = _visitedPositions[number - 1];

        var minX = int.MaxValue;
        var minY = int.MaxValue;
        var maxX = int.MinValue;
        var maxY = int.MinValue;

        foreach (var visitedPosition in visitedPositions)
        {
            minX = Math.Min(minX, visitedPosition.X);
            maxX = Math.Max(maxX, visitedPosition.X);
            minY = Math.Min(minY, visitedPosition.Y);
            maxY = Math.Max(maxY, visitedPosition.Y);
        }

        for (int y = maxY; y >= minY; y--)
        {
            for (int x = minX; x <= maxX; x++)
            {
                var marker = new Marker(x, y);
                Console.Write(visitedPositions.Contains(marker) ? "#" : ".");
            }
            Console.WriteLine();
        }
        Console.WriteLine($"X: {minX} - {maxX}, Y: {minY} - {maxY}");
    }
}
