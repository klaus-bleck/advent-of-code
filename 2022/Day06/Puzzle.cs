using AdventOfCode.Utils;

namespace AdventOfCode.Day06;

internal class Puzzle : IPuzzle
{
    private const int StartOfPacketMarkerLength = 4;
    private const int StartOfMessageMarkerLength = 14;
    private static IEnumerable<string> GetInput() => InputProvider.Iterate(6);

    private static int GetPositionOfStartOfPacketMarker(int startOfPacketMarkerLength)
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

    public object SolveFirst() => GetPositionOfStartOfPacketMarker(StartOfPacketMarkerLength);
    public object SolveSecond() => GetPositionOfStartOfPacketMarker(StartOfMessageMarkerLength);
}
