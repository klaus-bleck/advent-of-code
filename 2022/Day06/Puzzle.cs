namespace AdventOfCode.Day06;

public class Puzzle : PuzzleBase
{
    private const int StartOfPacketMarkerLength = 4;
    private const int StartOfMessageMarkerLength = 14;

    protected override uint Day => 6;

    private int GetPositionOfStartOfPacketMarker(int startOfPacketMarkerLength)
    {
        var line = GetInput().First();
        var slidingWindow = new LinkedList<char>();
        var differenceSet = new HashSet<char>();

        for (int i = 0; i < line.Length; i++)
        {
            var character = line[i];
            slidingWindow.AddLast(character);

            if (slidingWindow.Count >= startOfPacketMarkerLength)
            {
                differenceSet.Clear();
                foreach(var charInWindow in slidingWindow)
                {
                    differenceSet.Add(charInWindow);
                }

                if (differenceSet.Count == startOfPacketMarkerLength)
                {
                    return i + 1;
                }
                slidingWindow.RemoveFirst();
            }
        }
        return -1;
    }

    public override object SolveFirst() => GetPositionOfStartOfPacketMarker(StartOfPacketMarkerLength);
    public override object SolveSecond() => GetPositionOfStartOfPacketMarker(StartOfMessageMarkerLength);
}
