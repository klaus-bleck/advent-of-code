namespace AdventOfCode.Day13;

public sealed record Packet : IComparable<Packet>
{
    private readonly ListPacketValue _list;

    public Packet(ListPacketValue list)
    {
        _list = list;
    }

    private static int CompareTwoIntegers(IntegerPacketValue a, IntegerPacketValue b) =>
        a.Value.CompareTo(b.Value);

    private int CompareTwoLists(ListPacketValue a, ListPacketValue b)
    {
        for (int i = 0; i < a.Values.Count; i++)
        {
            if (i == b.Values.Count)
            {
                return 1;
            }

            var itemA = a.Values[i];
            var itemB = b.Values[i];
            var compared = CompareUnspecified(itemA, itemB);

            if (compared != 0)
            {
                return compared;
            }
        }
        return a.Values.Count < b.Values.Count ? -1 : 0;
    }

    private int CompareMixed(IntegerPacketValue a, ListPacketValue b)
    {
        var list = new ListPacketValue();
        list.Values.Add(a);
        return CompareTwoLists(list, b);
    }

    private int CompareMixed(ListPacketValue a, IntegerPacketValue b)
    {
        var list = new ListPacketValue();
        list.Values.Add(b);
        return CompareTwoLists(a, list);
    }

    private int CompareUnspecified(IPacketValue? x, IPacketValue? y)
    {
        if (x is ListPacketValue listA)
        {
            return y is ListPacketValue listB ?
                CompareTwoLists(listA, listB) :
                CompareMixed(listA, (IntegerPacketValue)y!);
        }
        else if (x is IntegerPacketValue intA)
        {
            return y is IntegerPacketValue intB ?
                CompareTwoIntegers(intA, intB) :
                CompareMixed(intA, (ListPacketValue)y!);
        }
        return 0;
    }

    public int CompareTo(Packet? other)
    {
        if (other == null)
        {
            return 1;
        }
        return CompareTwoLists(_list, other._list);
    }

    public string Print() => _list.Print();
}
