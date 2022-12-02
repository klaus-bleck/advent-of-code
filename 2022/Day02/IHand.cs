namespace AdventOfCode.Day02;

internal interface IHand
{
    Score Value { get; }
    HandResult Versus(IHand otherHand);
}
