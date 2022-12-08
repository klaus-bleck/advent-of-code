namespace AdventOfCode.Day08;

internal class Grid
{
    private readonly Tree[][] _trees;

    private Grid(Tree[][] trees) => _trees = trees;

    private static Tree[][] ParseTrees(IEnumerable<string> gridOutput) =>
        gridOutput.Select(x => x.AsEnumerable().Select(x => new Tree(int.Parse(x.ToString()))).ToArray()).ToArray();

    private void Iterate(Action<int, int> action)
    {
        for (int i = 0; i < _trees.Length; i++)
        {
            for (int j = 0; j < _trees[i].Length; j++)
            {
                action(i, j);
            }
        }
    }

    private bool IsVisible(int row, int column)
    {
        var tree = _trees[row][column];
        if (row == 0 || column == 0 || row == _trees.Length - 1 || column == _trees[row].Length - 1)
        {
            return true;
        }

        var left = true;
        var right = true;
        var upper = true;
        var lower = true;

        for (int rowIndex = 0; rowIndex < _trees.Length; rowIndex++)
        {
            var otherTree = _trees[rowIndex][column];
            if (otherTree.Height >= tree.Height)
            {
                if (rowIndex < row)
                {
                    upper = false;
                    rowIndex = row;
                }
                else
                {
                    lower = false;
                    break;
                }
            }
        }

        for (int columnIndex = 0; columnIndex < _trees[row].Length; columnIndex++)
        {
            var otherTree = _trees[row][columnIndex];
            if (otherTree.Height >= tree.Height)
            {
                if (columnIndex < column)
                {
                    left = false;
                    columnIndex = column;
                }
                else
                {
                    right = false;
                    break;
                }
            }
        }
        return left || right || upper || lower;
    }

    private int GetScenicScore(int row, int column)
    {
        var tree = _trees[row][column];

        var left = 0;
        var right = 0;
        var upper = 0;
        var lower = 0;

        static bool Process(Tree mainTree, in Tree otherTree, ref int score)
        {
            score++;
            return otherTree.Height >= mainTree.Height;
        }

        for (int i = row - 1; i >= 0; i--)
        {
            if (Process(tree, _trees[i][column], ref upper))
            {
                break;
            }
        }
        for (int i = row + 1; i < _trees.Length; i++)
        {
            if (Process(tree, _trees[i][column], ref lower))
            {
                break;
            }
        }
        for (int i = column - 1; i >= 0; i--)
        {
            if (Process(tree, _trees[row][i], ref left))
            {
                break;
            }
        }
        for (int i = column + 1; i < _trees[row].Length; i++)
        {
            if (Process(tree, _trees[row][i], ref right))
            {
                break;
            }
        }
        return upper * lower * left * right;
    }

    public static Grid Parse(IEnumerable<string> gridOutput) => new(ParseTrees(gridOutput));

    public int CalculateVisibleTrees()
    {
        int count = 0;
        Iterate((row, column) =>
        {
            if (IsVisible(row, column))
            {
                count++;
            }
        });
        return count;
    }

    public int CalculateHighestScore()
    {
        int highestScore = 0;
        Iterate((row, column) => highestScore = Math.Max(highestScore, GetScenicScore(row, column)));
        return highestScore;
    }
}
