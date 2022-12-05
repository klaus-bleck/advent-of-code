namespace AdventOfCode.Day05;

internal class CrateMatrix
{
    private const int CrateLength = 3;
    private const int CrateLengthWithSpace = CrateLength + 1;

    private readonly Stack<Crate>[] _stacks;

    public CrateMatrix(string[] input)
    {
        _stacks = CreateStacks(input);
    }

    private static void FillQueueList(string line, Stack<Crate>[] crateQueues)
    {
        for (int i = 0; i < line.Length; i += CrateLengthWithSpace)
        {
            var index = i / CrateLengthWithSpace;
            var queue = crateQueues[index];
            if (queue is null)
            {
                queue = new Stack<Crate>();
                crateQueues[index] = queue;
            }

            if (line[i] is '[')
            {
                queue.Push(new(line[i + 1]));
            }
        }
    }

    private static Stack<Crate>[] CreateStacks(string[] input)
    {
        var queueList = new Stack<Crate>[input[0].Length / CrateLengthWithSpace + 1];
        foreach (var line in input)
        {
            FillQueueList(line, queueList);
        }

        return queueList.Select(localStack =>
        {
            var stack = new Stack<Crate>();
            while (localStack.Count > 0)
            {
                stack.Push(localStack.Pop());
            }
            return stack;
        }).ToArray();
    }

    public void Apply(Instruction instruction)
    {
        for (int i = 0; i < instruction.Amount; i++)
        {
            var crate = _stacks[instruction.SourceIndex].Pop();
            _stacks[instruction.TargetIndex].Push(crate);
        }
    }

    public void ApplyBlock(Instruction instruction)
    {
        var localStack = new Stack<Crate>();
        for (int i = 0; i < instruction.Amount; i++)
        {
            localStack.Push(_stacks[instruction.SourceIndex].Pop());
        }

        for (int i = 0; i < instruction.Amount; i++)
        {
            _stacks[instruction.TargetIndex].Push(localStack.Pop());
        }
    }

    public string GetTopCrates() =>
        string.Concat(_stacks.Select(x => x.Peek()).Select(x => x.Value));
}
