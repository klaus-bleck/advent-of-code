namespace AdventOfCode.Day05;

public class Puzzle : PuzzleBase
{
    protected override uint Day => 5;

    private enum ParseMode
    {
        Crates,
        Instructions,
    }

    private static Instruction ParseInstruction(string line)
    {
        var numbers = line.Split(" ")
            .Select(x =>
            {
                var isNumber = int.TryParse(x, out int number);
                return (isNumber, number);
            })
            .Where(x => x.isNumber)
            .Select(x => x.number)
            .ToArray();
        return new Instruction(numbers[0], numbers[1], numbers[2]);
    }

    private static (CrateMatrix, Instruction[]) Parse(IEnumerable<string> input)
    {
        var parseMode = ParseMode.Crates;
        var crateMatrixBuilder = new CrateMatrixBuilder();
        var instructions = new List<Instruction>();

        foreach (var line in input)
        {
            if (line.Length == 0)
            {
                parseMode = ParseMode.Instructions;
                continue;
            }

            switch (parseMode)
            {
                case ParseMode.Crates:
                    crateMatrixBuilder.Add(line); break;
                case ParseMode.Instructions:
                    instructions.Add(ParseInstruction(line)); break;
            }
        }
        return (crateMatrixBuilder.Build(), instructions.ToArray());
    }

    private string GetTopCrates(Action<CrateMatrix, Instruction> action)
    {
        var (crateMatrix, instructions) = Parse(GetInput());
        foreach(var instruction in instructions)
        {
            action(crateMatrix, instruction);
        }
        return crateMatrix.GetTopCrates();
    }

    public override object SolveFirst() => GetTopCrates((crateMatrix, instruction) => crateMatrix.Apply(instruction));
    public override object SolveSecond() => GetTopCrates((crateMatrix, instruction) => crateMatrix.ApplyBlock(instruction));
}
