namespace AdventOfCode.Day12;

internal record struct Square (int Elevation, SquareType Type, int X, int Y)
{
    public static bool operator >(Square s1, Square s2) => s1.Elevation > s2.Elevation;
    public static bool operator <(Square s1, Square s2) => s1.Elevation < s2.Elevation;
}
