namespace AdventOfCode.Day02;

internal record Round(IHand Enemy, IHand Own)
{
    public Score Play() 
    {
        var result = Own.Play(Enemy);
        return new(result.Score.Value + Own.Value.Value);
    }
}
