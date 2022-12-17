namespace AdventOfCode.Day11;

public class Puzzle : PuzzleBase
{
    protected override uint Day => 11;

    private static ulong GetMonkeyItemReduceProduct(Monkey[] monkeys) =>
        monkeys.Select(x => x.Test.DivisibleBy).Aggregate((a, b) => a * b);

    private ulong CalculateMonkeyBusiness(int rounds, ulong divider)
    {
        var monkeys = MonkeyParser.Parse(GetInput());
        var reduceProduct = GetMonkeyItemReduceProduct(monkeys);

        for (int r = 0; r < rounds; r++)
        {
            foreach (var monkey in monkeys)
            {
                while(monkey.Items.Count > 0)
                {
                    var item = monkey.Inspect() / divider;
                    var nextMonkey = monkey.Throw(item);
                    monkeys[nextMonkey].Items.Enqueue(item % reduceProduct);
                }
            }
        }

        return monkeys
            .OrderByDescending(x => x.Inspected)
            .Take(2)
            .Select(x => x.Inspected)
            .Aggregate((x, y) => x * y);
    }

    public override object SolveFirst() => CalculateMonkeyBusiness(20, 3L);
    public override object SolveSecond() => CalculateMonkeyBusiness(10_000, 1L);
}
