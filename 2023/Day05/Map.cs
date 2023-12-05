namespace AdventOfCode.Day05;

public record Map(string Name, Mapping[] Mappings)
{
    private long FilterCore(IEnumerable<Mapping> mappings, long value) =>
        mappings.FirstOrDefault(x => x.IsValid(value))?.Map(value) ?? value;

    public long Filter(long value) => FilterCore(Mappings, value);

    public long FilterReverse(long value) => FilterCore(Mappings.Select(x => x.Reverse()), value);
}
