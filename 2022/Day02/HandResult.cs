namespace AdventOfCode.Day02;

internal abstract class HandResult
{
    public abstract Score Score { get; }

    public static HandResult Win { get; } = new WinResult();
    public static HandResult Lose { get; } = new LoseResult();
    public static HandResult Draw { get; } = new DrawResult();
}

internal sealed class WinResult : HandResult
{
    public override Score Score => new(6);
}

internal sealed class DrawResult : HandResult
{
    public override Score Score => new(3);
}

internal sealed class LoseResult : HandResult
{
    public override Score Score => new(0);
}
