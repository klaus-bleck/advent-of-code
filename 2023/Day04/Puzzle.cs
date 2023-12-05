namespace AdventOfCode.Day04;

public class Puzzle : PuzzleBase
{
    protected override uint Day => 4;

    private static CardGame Parse(string line)
    {
        var span = line.AsSpan();
        var cardNumberSeparator = span.IndexOf(':');
        var numberSetSeparator = span.IndexOf('|');
        var spaceSeparator = span.IndexOf(' ');

        var index = int.Parse(span[(spaceSeparator + 1)..cardNumberSeparator]);
        var winningNumbers = span[(cardNumberSeparator + 1)..numberSetSeparator].ToString()
            .Split(" ", StringSplitOptions.RemoveEmptyEntries);
        var playerNumbers = span[(numberSetSeparator + 1)..^0].ToString()
            .Split(" ", StringSplitOptions.RemoveEmptyEntries);
        return new(index, winningNumbers, playerNumbers);
    }

    private static int GetCardPoints(string line)
    {
        var cardGame = Parse(line);
        return cardGame.Matches.Length > 0 ? 1 << (cardGame.Matches.Length - 1) : 0;
    }

    private static long GetCardCount(IEnumerable<string> lines)
    {
        List<CardGame> cardGames = lines.Select(Parse).ToList();
        Dictionary<int, int> gamesVisited = [];

        void MarkGame(CardGame game, int count)
        {
            if (!gamesVisited.ContainsKey(game.Index))
            {
                gamesVisited[game.Index] = 0;
            }
            gamesVisited[game.Index] += count;
        }

        foreach (var game in cardGames)
        {
            MarkGame(game, 1);

            Console.WriteLine("Index " + game.Index + " with " + game.Matches.Length + " matches");
            if (game.Matches.Length > 0 && game.Index <= cardGames.Count)
            {
                Console.WriteLine("copy for " + (game.Index + 1) + ".." + (Math.Min(game.Index + game.Matches.Length, cardGames.Count)));

                var copies = cardGames[game.Index..Math.Min(game.Index + game.Matches.Length, cardGames.Count)];
                foreach (var copy in copies)
                {
                    MarkGame(copy, gamesVisited[game.Index]);
                }
                Console.WriteLine();
            }
        }
        return gamesVisited.Values.Sum();
    }

    public override object SolveFirst() => GetInput().Sum(GetCardPoints);

    public override object SolveSecond() => GetCardCount(GetInput());
}
