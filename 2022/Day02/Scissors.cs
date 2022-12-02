namespace AdventOfCode.Day02;

internal class Scissors : IHand
{
    public Score Value => new(3);

    public HandResult Versus(IHand otherHand)
    {
        return otherHand switch
        {
            Rock => HandResult.Lose,
            Paper => HandResult.Win,
            Scissors => HandResult.Draw,
            _ => throw new NotSupportedException($"Hand {otherHand} is not supported."),
        };
    }
}
