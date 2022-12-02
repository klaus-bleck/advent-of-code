namespace AdventOfCode.Day02;

internal abstract class HandResult
{
    public abstract Score Score { get; }

    public static HandResult Win { get; } = new WinResultState();
    public static HandResult Lose { get; } = new LoseResultState();
    public static HandResult Draw { get; } = new DrawResultState();
}

internal sealed class WinResultState : HandResult
{
    public override Score Score => new(6);
}

internal sealed class DrawResultState : HandResult
{
    public override Score Score => new(3);
}

internal sealed class LoseResultState : HandResult
{
    public override Score Score => new(0);
}
