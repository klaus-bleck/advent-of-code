namespace AdventOfCode.Day10;

internal sealed class Crt
{
    private const int Width = 40;
    private const int Height = 6;
    private const string VisibleSymbol = "#";
    private const string InvisibleSymbol = ".";

    private int _currentPixel = 0;

    private bool IsVisible(int registerValue) =>
        _currentPixel >= registerValue - 1 && _currentPixel <= registerValue + 1;

    public void Draw(Register register)
    {
        Console.Write(IsVisible(register.Value) ? VisibleSymbol : InvisibleSymbol);
        if (++_currentPixel % Width == 0)
        {
            _currentPixel = 0;
            Console.WriteLine();
        }
    }
}
