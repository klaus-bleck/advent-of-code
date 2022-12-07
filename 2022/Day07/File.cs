namespace AdventOfCode.Day07;

internal sealed class File
{
    public string Name { get; }
    public ulong Size { get; }

    public File(string name, ulong size)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Size = size;
    }
}
