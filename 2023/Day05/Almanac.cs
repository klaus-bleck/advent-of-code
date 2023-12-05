namespace AdventOfCode.Day05;

public record Almanac(long[] Seeds, Map[] Maps)
{
    private long GetLowestLocationForSeedSequence(IEnumerable<long> sequence) =>
        sequence.Min(x => Maps.Aggregate(x, (accumulator, map) => map.Filter(accumulator)));

    private long GetFirstMatchingSeed(IEnumerable<long> sequence)
    {
        // quite simple backtracking... optimizable for sure
        var pairs = Pair();
        var reversedMaps = Maps.Reverse();
        return sequence.First(x => {
            var filtered = reversedMaps.Aggregate(x, (accumulator, map) => map.FilterReverse(accumulator));
            return pairs.Any(p => filtered >= p.PairStart && filtered <= p.PairEnd);
        });
    }       

    private IEnumerable<(long PairStart, long PairEnd)> Pair()
    {
        long start = 0L;
        for (int i = 0; i < Seeds.Length; i++)
        {
            if (i % 2 == 0)
            {
                start = Seeds[i];
            }
            else
            {
                yield return (start, start + Seeds[i] - 1);
            }
        }
    }

    private IEnumerable<long> Backtracking()
    {
        long location = 0L;
        while (true)
        {
            yield return location++;
        }
    }

    public long GetLowestLocation() => GetLowestLocationForSeedSequence(Seeds);

    public long GetLowestLocationWithPairs() => GetFirstMatchingSeed(Backtracking());

}
