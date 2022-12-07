namespace AdventOfCode.Day07;

internal sealed class FileSystem
{
    private const string RootPath = "/";
    private const string MoveOneLevelOutPath = "..";

    private readonly Directory _root;
    private readonly Stack<Directory> _directoryStack;

    public Directory CurrentDirectory => _directoryStack.Peek();
    public ulong AvailableSpace => 70000000UL;
    public ulong UsedSpace => _root.CalculateSize();
    public ulong UnusedSpace => AvailableSpace - UsedSpace;

    public FileSystem()
    {
        _root = new(RootPath);
        _directoryStack = new();
        _directoryStack.Push(_root);
    }

    private void GoBackToRoot()
    {
        while (_directoryStack.Count > 1)
        {
            _directoryStack.Pop();
        }
    }

    public void ChangeDirectory(string path)
    {
        ArgumentNullException.ThrowIfNull(path);

        if (path == RootPath)
        {
            GoBackToRoot();
        }
        else if (path == MoveOneLevelOutPath)
        {
            _directoryStack.Pop();
        }
        else
        {
            var directory = CurrentDirectory.GetDirectory(path);
            _directoryStack.Push(directory);
        }
    }
}
