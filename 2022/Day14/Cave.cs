namespace AdventOfCode.Day14;

internal sealed class Cave
{
    private Position _upperLeft;
    private Position _lowerRight;
    private readonly Position _start;
    private readonly Dictionary<Position, Element> _placementMap;
    private int? _floor;

    public Cave(Position start, IEnumerable<Placement> placements)
    {
        _start = start;
        _placementMap = new();

        DetermineSizes(placements);
        AddPlacements(placements.Union(new Placement[] { new(Element.SandSource, _start) }));
    }

    private void DetermineSizes(IEnumerable<Placement> placements)
    {
        var minX = int.MaxValue;
        var maxX = int.MinValue;
        var minY = int.MaxValue;
        var maxY = int.MinValue;

        foreach (var position in placements.Select(x => x.Position))
        {
            minX = Math.Min(minX, position.X);
            maxX = Math.Max(maxX, position.X);
            minY = Math.Min(minY, position.Y);
            maxY = Math.Max(maxY, position.Y);
        }

        _upperLeft = new Position(minX, minY);
        _lowerRight = new Position(maxX, maxY);
    }

    private void AddPlacements(IEnumerable<Placement> placements)
    {
        foreach (var placement in placements)
        {
            _placementMap[placement.Position] = placement.Element;
        }
    }

    private SimulationResult Simulate(in Position position)
    {
        var newPosition = new Position(position.X, position.Y + 1);
        if (newPosition.Y <= _lowerRight.Y)
        {
            if (newPosition.Y == _floor)
            {
                return new(SimulationState.Rest, position);
            }

            if (!_placementMap.TryGetValue(newPosition, out var _))
            {
                return new(SimulationState.Down, newPosition);
            }

            newPosition = new Position(position.X - 1, newPosition.Y);
            if (!_floor.HasValue && newPosition.X < 0)
            {
                return new(SimulationState.InAbyss, newPosition);
            }
            else if (!_placementMap.TryGetValue(newPosition, out var _))
            {
                return new(SimulationState.DownLeft, newPosition);
            }

            newPosition = new Position(position.X + 1, newPosition.Y);
            if (!_floor.HasValue && newPosition.X > _lowerRight.X)
            {
                return new(SimulationState.InAbyss, newPosition);
            }
            else if (!_placementMap.TryGetValue(newPosition, out var _))
            {
                return new(SimulationState.DownRight, newPosition);
            }
            return new(SimulationState.Rest, position);
        }
        return new(SimulationState.InAbyss, newPosition);
    }

    public IEnumerable<SimulationResult> Simulate(Predicate<SimulationResult> condition)
    {
        SimulationResult result = new(SimulationState.Created, _start);
        bool shouldSimulate;

        do
        {
            result = Simulate(result.Position);
            shouldSimulate = condition(result);
            yield return result;

            if (result.State == SimulationState.Rest)
            {
                _placementMap[result.Position] = Element.Sand;
                result = new(SimulationState.Created, _start);
            }
        }
        while (shouldSimulate);

    }

    public Cave WithFloor(int offset)
    {
        _floor = _lowerRight.Y + offset;
        _lowerRight = new(_lowerRight.X, _floor.Value);
        return this;
    }

    public void Draw()
    {
        var colStart = Math.Min(_upperLeft.X, _placementMap.Keys.Min(x => x.X));
        var colEnd = Math.Max(_lowerRight.X, _placementMap.Keys.Max(x => x.X));
        var rowStart = Math.Min(_upperLeft.Y, _placementMap.Keys.Min(x => x.Y));
        var rowEnd = Math.Max(_lowerRight.Y, _placementMap.Keys.Max(x => x.Y));

        for (int row = rowStart; row <= rowEnd; row++)
        {
            for (int column = colStart; column <= colEnd; column++)
            {
                Element element = Element.Air;
                if (row == _floor)
                {
                    element = Element.Rock;
                }
                else
                {
                    _placementMap.TryGetValue(new(column, row), out element);
                }

                var symbol = element switch
                {
                    Element.SandSource => '+',
                    Element.Sand => 'o',
                    Element.Rock => '#',
                    _ => '.',
                };
                Console.Write(symbol);
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}
