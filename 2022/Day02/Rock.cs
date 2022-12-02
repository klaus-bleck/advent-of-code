namespace AdventOfCode.Day02;

internal class Rock : IHand
{
    public Score Value => new(1);

    public HandResult Versus(IHand otherHand)
    {
        return otherHand switch
        {
            Rock => HandResult.Draw,
            Paper => HandResult.Lose,
            Scissors => HandResult.Win,
            _ => throw new NotSupportedException($"Hand {otherHand} is not supported."),
        };
    }
}
