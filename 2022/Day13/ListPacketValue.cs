namespace AdventOfCode.Day13;

public class ListPacketValue : IPacketValue
{
    public List<IPacketValue> Values { get; }

    public ListPacketValue()
    {
        Values = new();
    }

    public string Print() =>
        $"[{string.Join(",", Values.Select(x => x.Print()))}]";
}
