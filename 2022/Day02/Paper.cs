namespace AdventOfCode.Day02;

internal class Paper : IHand
{
    public Score Value => new(2);

    public HandResult Play(IHand otherHand)
    {
        return otherHand switch
        {
            Rock => HandResult.Win,
            Paper => HandResult.Draw,
            Scissors => HandResult.Lose,
            _ => throw new NotSupportedException($"Hand {otherHand} is not supported."),
        };
    }
}
