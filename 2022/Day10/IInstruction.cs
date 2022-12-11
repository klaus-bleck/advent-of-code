namespace AdventOfCode.Day10;

internal interface IInstruction
{
    static IInstruction FromString(string text)
    {
        var parts = text.Split(' ');
        return parts switch
        {
            ["noop"] => new NoopInstruction(),
            ["addx", var value] => new AddxInstruction(int.Parse(value)),
            _ => throw new NotSupportedException($"Instruction {parts[0]} is not supported.")
        }; ;
    }

    IEnumerable<bool> Execute(Func<int> loadRegisterValueFunc);
    void Apply(Action<int> storeRegisterValueAction);
}

internal sealed class NoopInstruction : IInstruction
{
    public IEnumerable<bool> Execute(Func<int> loadRegisterValueFunc)
    {
        yield return true;
    }

    public void Apply(Action<int> storeRegisterValueAction) { }
}

internal sealed class AddxInstruction : IInstruction
{
    private readonly int _value;
    private int _result;

    public AddxInstruction(int value) => _value = value;

    public IEnumerable<bool> Execute(Func<int> loadRegisterValueFunc)
    {
        var registerValue = loadRegisterValueFunc();
        yield return false;

        _result = registerValue + _value;
        yield return true;
    }

    public void Apply(Action<int> storeRegisterValueAction) =>
        storeRegisterValueAction(_result);
}