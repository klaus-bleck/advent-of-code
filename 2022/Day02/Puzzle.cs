using AdventOfCode.Utils;

namespace AdventOfCode.Day02;

public class Puzzle : IPuzzle
{
    private static IHand ParseHand(string enemyHand) =>
        enemyHand switch
        {
            "A" or "X" => new Rock(),
            "B" or "Y" => new Paper(),
            "C" or "Z" => new Scissors(),
            _ => throw new NotSupportedException($"Hand {enemyHand} is not supported."),
        };

    private static IHand ExtractHand(string enemyHand, string handResult) =>
        (enemyHand, handResult) switch
        {
            ("A", "X") or ("C", "Y") or ("B", "Z") => new Scissors(),
            ("B", "X") or ("A", "Y") or ("C", "Z") => new Rock(),
            ("C", "X") or ("B", "Y") or ("A", "Z") => new Paper(),
            _ => throw new NotSupportedException($"Combination ({enemyHand}, {handResult}) is not supported."),
        };

    private static Round ParseRound(string[] line) =>
        new(ParseHand(line[0]), ParseHand(line[1]));

    private static Round ParseRoundByExtracting(string[] line) =>
        new(ParseHand(line[0]), ExtractHand(line[0], line[1]));

    private static int GetTotalScore(Func<string[], Round> calculator)
    {
        return InputProvider.Iterate(2)
            .Select(x => calculator(x.Split(" ")))
            .Sum(x => x.Play().Value);
    }

    public object SolveFirst() => GetTotalScore(ParseRound);
    public object SolveSecond() => GetTotalScore(ParseRoundByExtracting);
}
