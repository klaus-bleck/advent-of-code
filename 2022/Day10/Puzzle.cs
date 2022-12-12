using AdventOfCode.Utils;

namespace AdventOfCode.Day10;

internal class Puzzle : IPuzzle
{
    private static IEnumerable<string> GetInput() => InputProvider.Iterate(10);
    private static IEnumerable<string> GetSample() => InputProvider.IterateSample(10);

    public object SolveFirst()
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

    public object SolveSecond()
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
