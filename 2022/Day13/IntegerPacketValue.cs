namespace AdventOfCode.Day13;

public sealed record IntegerPacketValue(int Value) : IPacketValue
{
    public string Print() => Value.ToString();
}
