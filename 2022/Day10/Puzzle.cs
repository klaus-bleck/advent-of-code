namespace AdventOfCode.Day10;

public class Puzzle : PuzzleBase
{
    protected override uint Day => 10;

    public override object SolveFirst()
    {
        var cpu = new Cpu(GetInput());
        var result = 0L;
        var relevantTicks = new HashSet<int>
        {
            20, 60, 100, 140, 180, 220
        };

        foreach (var tick in cpu.Tick().Where(t => relevantTicks.Contains(t)))
        {
            result += (tick * cpu.Register.Value);
        }
        return result;
    }

    public override object SolveSecond()
    {
        var cpu = new Cpu(GetInput());
        var crt = new Crt();

        foreach (var _ in cpu.Tick())
        {
            crt.Draw(cpu.Register);
        }
        return 0;
    }
}
