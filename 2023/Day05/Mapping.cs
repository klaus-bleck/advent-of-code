namespace AdventOfCode.Day05;

public record Mapping(long SourceStart, long Range, long DestinationStart)
{
    public long SourceEnd => SourceStart + Range - 1;

    public bool IsValid(long value) => value >= SourceStart && value <= SourceEnd;

    public long Map(long value) => DestinationStart + value - SourceStart;

    public Mapping Reverse() => this with { SourceStart = DestinationStart, DestinationStart = SourceStart };
}
