namespace AdventOfCode.Day07;

public class Puzzle : PuzzleBase
{
    protected override uint Day => 7;

    private static bool IsCommand(string input) => input.StartsWith("$");

    private static (string Command, string? Parameter) GetCommand(string input)
    {
        var commandParts = input.Split(" ");
        var parameter = commandParts.Length > 2 ? commandParts[2] : null;
        return (commandParts[1], parameter);
    }

    private static void ApplyCommand(FileSystem fileSystem, string command, string? parameter)
    {
        switch(command)
        {
            case "cd": fileSystem.ChangeDirectory(parameter!); break;
        }
    }

    private static void AddToFileSystem(FileSystem fileSystem, string input)
    {
        var parts = input.Split(" ");
        var currentDirectory = fileSystem.CurrentDirectory;

        switch(parts[0])
        {
            case "dir": currentDirectory.AddDirectory(new(parts[1])); break;
            default: currentDirectory.AddFile(new(parts[1], ulong.Parse(parts[0]))); break;
        }
    }

    private FileSystem GetFileSystem()
    {
        var fileSystem = new FileSystem();
        string? lastCommand = null;
        foreach (var input in GetInput())
        {
            if (IsCommand(input))
            {
                var (command, parameter) = GetCommand(input);
                ApplyCommand(fileSystem, command, parameter);
                lastCommand = command;
            }
            else if(lastCommand is "ls")
            {
                AddToFileSystem(fileSystem, input);
            }
        }

        fileSystem.ChangeDirectory("/");
        return fileSystem;
    }

    private ulong GetTotalSize()
    {
        var fileSystem = GetFileSystem();
        var totalSize = 0UL;
        foreach(var directory in fileSystem.CurrentDirectory)
        {
            var directorySize = directory.CalculateSize();
            if (directorySize <= 100000UL)
            {
                totalSize += directorySize;
            }
        }
        return totalSize;
    }

    private ulong GetTotalSizeForUpdate()
    {
        var fileSystem = GetFileSystem();
        var updateSize = 30000000UL;
        var toDelete = updateSize - fileSystem.UnusedSpace;
        var totalSize = fileSystem.UsedSpace;
        foreach (var directory in fileSystem.CurrentDirectory)
        {
            var directorySize = directory.CalculateSize();
            if (directorySize > toDelete)
            {
                totalSize = Math.Min(directorySize, totalSize);
            }
        }
        return totalSize;
    }

    public override object SolveFirst() => GetTotalSize();
    public override object SolveSecond() => GetTotalSizeForUpdate();
}
