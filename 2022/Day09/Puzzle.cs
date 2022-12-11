using AdventOfCode.Utils;

namespace AdventOfCode.Day09;

internal class Puzzle : IPuzzle
{
    private static IEnumerable<string> GetInput() => InputProvider.Iterate(9);
    private static IEnumerable<string> GetSample() => InputProvider.IterateSample(9);

    private static void ApplyCommand(Grid grid, string command, int amount)
    {
        switch (command)
        {
            case "L": grid.MoveLeft(amount); break;
            case "R": grid.MoveRight(amount); break;
            case "U": grid.MoveUp(amount); break;
            case "D": grid.MoveDown(amount); break;
            default: throw new NotSupportedException($"Command {command} is not supported.");
        }
    }

    private static int GetVisitedPositionsByKnot(int knots)
    {
        var grid = new Grid(knots);
        foreach (var command in GetInput())
        {
            var commandPart = command.Split(' ');
            var amount = int.Parse(commandPart[1]);
            ApplyCommand(grid, commandPart[0], amount);            
        }        
        return grid.GetVisitedMarkerForKnot(knots);
    }

    public object SolveFirst() => GetVisitedPositionsByKnot(2);
    public object SolveSecond() => GetVisitedPositionsByKnot(10);
}
