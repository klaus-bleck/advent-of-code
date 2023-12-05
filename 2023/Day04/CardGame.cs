namespace AdventOfCode.Day04;

public record CardGame(int Index, string[] WinningNumbers, string[] PlayerNumbers)
{
    public string[] Matches { get; } = WinningNumbers.Intersect(PlayerNumbers).ToArray();
}
