namespace AdventOfCode.Day10;

internal sealed class Register
{
    public int Value { get; private set; }

    public Register(int defaultValue) => Value = defaultValue;

    public int Load() => Value;

    public void Store(int value) => Value = value;
}
