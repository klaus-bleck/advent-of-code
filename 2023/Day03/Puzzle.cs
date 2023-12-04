using System.Globalization;

namespace AdventOfCode.Day03;

public class Puzzle : PuzzleBase
{
    protected override uint Day => 3;

    private static (Symbol[] Symbols, Number[] Numbers) Parse(string[] lines)
    {
        List<Symbol> symbols = [];
        List<Number> numbers = [];

        void AddNumber(List<(char Number, int Column)> numberData, int rowIndex)
        {
            var number = int.Parse(string.Join(string.Empty, numberData.Select(x => x.Number)));
            numbers.Add(new(number, rowIndex, numberData[0].Column, numberData[^1].Column));
            numberData.Clear();
        }

        for (int rowIndex = 0; rowIndex < lines.Length; rowIndex++)
        {
            var currentRow = lines[rowIndex];
            List<(char Number, int Column)> numberSequence = [];

            for (int columnIndex = 0; columnIndex < currentRow.Length; columnIndex++)
            {
                var currentCell = currentRow[columnIndex];
                if (char.IsNumber(currentCell))
                {
                    numberSequence.Add((currentCell, columnIndex));
                }
                else
                {
                    if (currentCell != '.')
                    {
                        symbols.Add(new(currentCell, new(rowIndex, columnIndex)));
                    }

                    if (numberSequence.Count > 0)
                    {
                        AddNumber(numberSequence, rowIndex);
                    }
                }
            }

            if (numberSequence.Count > 0)
            {
                AddNumber(numberSequence, rowIndex);
            }
        }
        return (symbols.ToArray(), numbers.ToArray());
    }

    private static int IsPartNumber(Number number, HashSet<Position> symbolSet) =>
        number.SelectMany(x => x.GetAdjacents()).Any(symbolSet.Contains) ? number.Value : 0;

    private static long GetGearRatio(Symbol symbol, Dictionary<Position, long> numberMap)
    {
        if (symbol.Value != '*')
        {
            return 0;
        }

        var intersections = symbol.Position.GetAdjacents().Intersect(numberMap.Keys).ToArray();
        var numbersOfIntersection = intersections.Select(x => numberMap[x]).Distinct().ToArray();
        if (numbersOfIntersection.Length == 2)
        {
            return numbersOfIntersection[0] * numbersOfIntersection[1];
        }
        return 0;
    }

    private static int GetSumOfPartNumbers(IEnumerable<string> lines)
    {
        (var symbols, var numbers) = Parse(lines.ToArray());
        var symbolSet = new HashSet<Position>(symbols.Select(x => x.Position));
        return numbers.Sum(x => IsPartNumber(x, symbolSet));
    }

    private static long GetProductOfGearRatio(IEnumerable<string> lines)
    {
        (var symbols, var numbers) = Parse(lines.ToArray());

        Dictionary<Position, long> numberMap = [];
        foreach (var number in numbers)
        {
            foreach (var numberPart in number)
            {
                numberMap[numberPart] = number.Value;
            }
        }
        return symbols.Sum(x => GetGearRatio(x, numberMap));
    }

    public override object SolveFirst() => GetSumOfPartNumbers(GetInput());

    public override object SolveSecond() => GetProductOfGearRatio(GetInput());
}
