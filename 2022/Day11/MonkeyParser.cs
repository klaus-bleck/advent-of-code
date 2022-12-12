using System.Linq.Expressions;

namespace AdventOfCode.Day11;

internal static class MonkeyParser
{
    private enum ParsePhase
    {
        Creating,
        StartingItems,
        Operation,
        TestDeclaration,
        TestTrue,
        TestFalse,
    }

    private static ParsePhase ParseStartingItems(Monkey monkey, string input)
    {
        var split = input.Split(":");
        var items = split[1].Trim().Split(",");
        foreach(var item in items.Select(ulong.Parse))
        {
            monkey.Items.Enqueue(item);
        }
        return ParsePhase.Operation;
    }

    private static ParsePhase ParseOperation(Monkey monkey, string input)
    {
        var split = input.Split("=");
        var expressionParts = split[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
        
        // TODO: Visitor or something...
        Expression left = ulong.TryParse(expressionParts[0], out var leftConstant) ? Expression.Constant(leftConstant) : Expression.Parameter(typeof(ulong), expressionParts[0]);
        Expression right = ulong.TryParse(expressionParts[2], out var rightConstant) ? Expression.Constant(rightConstant) : Expression.Parameter(typeof(ulong), expressionParts[2]);
        List<ParameterExpression> parameters = new();
        if (left is ParameterExpression lp)
        {
            parameters.Add(lp);
        }
        if (right is ParameterExpression rp)
        {
            parameters.Add(rp);
        }

        var operation = expressionParts[1] switch
        {
            "*" => Expression.Multiply(left, right),
            "+" => Expression.Add(left, right),
            _ => throw new NotSupportedException()
        };       

        monkey.SetOperation(Expression.Lambda(operation, parameters).Compile(), parameters.Count);
        return ParsePhase.TestDeclaration;
    }

    private static ParsePhase ParseTestDeclaration(Monkey monkey, string input)
    {
        var split = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        monkey.Test.DivisibleBy = ulong.Parse(split[3]);
        return ParsePhase.TestTrue;
    }    

    private static ParsePhase ParseTestTrue(Monkey monkey, string input)
    {
        var split = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        monkey.Test.NumberIfTrue = int.Parse(split[5]);
        return ParsePhase.TestFalse;
    }

    private static ParsePhase ParseTestFalse(Monkey monkey, string input)
    {
        var split = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        monkey.Test.NumberIfFalse = int.Parse(split[5]);
        return ParsePhase.Creating;
    }

    public static Monkey[] Parse(IEnumerable<string> _input)
    {
        var result = new List<Monkey>();
        var phase = ParsePhase.Creating;

        foreach (var input in _input.Where(x => x.Length > 0))
        {
            if (phase == ParsePhase.Creating)
            {
                var split = input.Split(' ');
                result.Add(new Monkey(int.Parse(split[1].Remove(split[1].Length - 1))));
                phase = ParsePhase.StartingItems;
            }
            else
            {
                var monkey = result[result.Count - 1];
                phase = phase switch
                {
                    ParsePhase.StartingItems => ParseStartingItems(monkey, input),
                    ParsePhase.Operation => ParseOperation(monkey, input),
                    ParsePhase.TestDeclaration => ParseTestDeclaration(monkey, input),
                    ParsePhase.TestTrue => ParseTestTrue(monkey, input),
                    ParsePhase.TestFalse => ParseTestFalse(monkey, input),
                    _ => throw new NotSupportedException(),
                };
            }
        }
        return result.OrderBy(x => x.Number).ToArray();
    }    
}
