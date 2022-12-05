namespace AdventOfCode.Day05;

internal readonly record struct Instruction(int Amount, int Source, int Target)
{
    public int SourceIndex => Source - 1;
    public int TargetIndex => Target - 1;
}
