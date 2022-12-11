namespace AdventOfCode.Day09;

internal record Position
{
    public static Position Zero => new(0, 0);    

    public int X { get; private set; }
    public int Y { get; private set; }

    public Position(int x, int y)
    {
        X = x; 
        Y = y;
    }

    public static Position operator +(Position left, Position right) => new(left.X + right.X, left.Y + right.Y);
    public static Position operator -(Position left, Position right) => new(left.X - right.X, left.Y - right.Y);
    
    public void Move(int horizontalOffset, int verticalOffset)
    {
        X += horizontalOffset;
        Y += verticalOffset;
    }

    public void Move(Position other) => Move(other.X, other.Y);
}
