namespace AdventOfCode.Day13;

public class Puzzle : PuzzleBase
{
    protected override uint Day => 13;

    private static Packet ParsePacket(string input)
    {
        var listStack = new Stack<ListPacketValue>();
        var numberBuffer = new List<char>();
        ListPacketValue? currentList = new();

        void ClearNumberBuffer()
        {
            if (numberBuffer.Any())
            {
                listStack.Peek().Values.Add(new IntegerPacketValue(int.Parse(new(numberBuffer.ToArray()))));
                numberBuffer.Clear();
            }
        }

        foreach (var character in input)
        {
            if (character == '[')
            {
                currentList = new();
                listStack.Push(currentList);
            }
            else if (character == ']')
            {
                ClearNumberBuffer();

                var lastList = listStack.Pop();
                if (listStack.TryPeek(out currentList))
                {
                    currentList.Values.Add(lastList);
                }
                else
                {
                    currentList = lastList;
                }
            }
            else if (character == ',')
            {
                ClearNumberBuffer();
            }
            else
            {
                numberBuffer.Add(character);
            }
        }
        return new(currentList);
    }

    private static Packet CreateDividerPacket(int value)
    {
        var listValue = new ListPacketValue();
        listValue.Values.Add(new IntegerPacketValue(value));
        var dividerA = new ListPacketValue();
        dividerA.Values.Add(listValue);
        return new Packet(dividerA);
    }

    private static Pair CreateDividerPair() =>
        new(CreateDividerPacket(2), CreateDividerPacket(6));

    private Pair[] ParsePairs()
    {
        List<Pair> result = new();
        List<Packet> lists = new();

        foreach (var line in GetInput())
        {
            if (line.Length == 0)
            {
                result.Add(new(lists[0], lists[1]));
                lists.Clear();
                continue;
            }
            lists.Add(ParsePacket(line));
        }
        result.Add(new(lists[0], lists[1]));
        return result.ToArray();
    }

    public override object SolveFirst() => 
        ParsePairs()
            .Select((x, i) => new { Pair = x, Index = i + 1 })
            .Where(x => x.Pair.IsInCorrectOrder())
            .Sum(x => x.Index);

    public override object SolveSecond()
    {
        var pairs = ParsePairs();
        var dividerPair = CreateDividerPair();
        var extendedPairs = new List<Pair>(pairs) { dividerPair };

        var flattened = extendedPairs.SelectMany(p => new Packet[] { p.A, p.B }).ToList();
        flattened.Sort();

        return flattened
            .Select((x, i) => new { Packet = x, Index = i + 1 })
            .Where(x => x.Packet == dividerPair.A || x.Packet == dividerPair.B)
            .Select(x => x.Index)
            .Aggregate((a, b) => a * b);
    }
}