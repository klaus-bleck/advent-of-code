namespace AdventOfCode.Day13;

public sealed record Pair(Packet A, Packet B)
{
    public bool IsInCorrectOrder() => 
        A.CompareTo(B) <= 0;
}