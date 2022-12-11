namespace AdventOfCode.Day10;

internal sealed class Cpu
{
    private readonly IEnumerable<string> _opCodeSource;

    public Register Register { get; }

    public Cpu(IEnumerable<string> opCodeSource)
    {
        _opCodeSource = opCodeSource;
        Register = new Register(1);
    }

    public IEnumerable<int> Tick()
    {
        IInstruction instruction;
        int cycle = 0;

        foreach(var opCode in _opCodeSource)
        {
            instruction = IInstruction.FromString(opCode);
            foreach (var _ in instruction.Execute(Register.Load))
            {
                yield return ++cycle;
            }
            instruction.Apply(Register.Store);
        }
    }
}
