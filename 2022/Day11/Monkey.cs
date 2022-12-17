namespace AdventOfCode.Day11;

internal sealed class Monkey
{
    private int _oldCount;

    public int Number { get; }
    public Queue<ulong> Items { get; } 
    public Delegate? Operation { get; private set; }
    public Test Test { get; }
    public ulong Inspected { get; private set; }

    public Monkey(int number)
    {
        Number = number;
        Items = new Queue<ulong>();
        Test = new Test();
    }

    private ulong Operate(object[] arguments) =>
        (ulong)(Operation?.DynamicInvoke(arguments) ?? throw new InvalidOperationException());

    public ulong Inspect()
    {
        Inspected++;        
        var item = Items.Dequeue();
        var oldParameter = new object[_oldCount];
        for (int i = 0; i < _oldCount; i++)
        {
            oldParameter[i] = item;
        }
        return Operate(oldParameter);
    }

    public int Throw(ulong item) => Test.Apply(item);

    public void SetOperation(Delegate operation, int oldCount)
    {
        Operation = operation;
        _oldCount = oldCount;
    }
}

internal sealed class Test
{
    public ulong DivisibleBy { get; set; }
    public int NumberIfTrue { get; set; }
    public int NumberIfFalse { get; set; }

    public int Apply(ulong item) =>
        item % DivisibleBy == 0 ? NumberIfTrue : NumberIfFalse;
}
