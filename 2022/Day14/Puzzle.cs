using Microsoft.Diagnostics.Runtime.Utilities;

namespace AdventOfCode.Day14;

public class Puzzle : PuzzleBase
{
    protected override uint Day => 14;

    private Path ParseRockPath(string line)
    {
        var path = new Path();
        path.Positions.AddRange
        (
            line
                .Split(" -> ")
                .Select(x =>
                {
                    var position = x.Split(",");
                    return new Position(int.Parse(position[0]), int.Parse(position[1]));
                })
        );
        return path;
    }

    private IEnumerable<Placement> ExtractPlacement(Element element, Path path)
    {
        var result = new List<Placement>();
        for (int i = 0; i < path.Positions.Count - 1; i++)
        {
            result.AddRange(ExtractPlacement(element, path.Positions[i], path.Positions[i + 1]));
        }
        return result;
    }

    private IEnumerable<Placement> ExtractPlacement(Element element, Position from, Position to)
    {
        var result = new List<Placement>();
        int steps;
        Position offset;
        
        var diffX = to.X - from.X;
        if (diffX != 0)
        {
            steps = Math.Abs(diffX);
            offset = new Position(diffX / steps, 0);
        }
        else
        {
            var diffY = to.Y - from.Y;
            steps = Math.Abs(diffY);
            offset = new Position(0, diffY / steps);
        }
       
        var current = from;
        for (int i = 0; i <= steps; i++)
        {
            result.Add(new Placement(element, current));
            current += offset;
        }
        return result;
    }

    private Cave ParseCave(Position start)
    {
        var placements = GetInput().Select(ParseRockPath).SelectMany(x => ExtractPlacement(Element.Rock, x)).ToList();
        return new Cave(start, placements);
    }

    public override object SolveFirst() =>
        ParseCave(new(500, 0)).Simulate(result => result.State != SimulationState.InAbyss).Count(x => x.State == SimulationState.Rest);

    public override object SolveSecond() =>
        ParseCave(new(500, 0)).WithFloor(2).Simulate(result => result.State != SimulationState.Rest || !result.Position.Equals(new(500, 0))).Count(x => x.State == SimulationState.Rest);
}