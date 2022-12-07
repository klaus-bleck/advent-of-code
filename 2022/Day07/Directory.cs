using System.Collections;

namespace AdventOfCode.Day07;

internal sealed class Directory : IEnumerable<Directory>
{
    private readonly List<Directory> _directories;
    private readonly List<File> _files;

    public string Name { get; }

    public Directory(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        _directories = new List<Directory>();
        _files = new List<File>();
    }

    private static IEnumerable<Directory> IterateDirectories(IEnumerable<Directory> directories)
    {
        foreach (var directory in directories)
        {
            yield return directory;
            foreach(var subDirectory in IterateDirectories(directory._directories))
            {
                yield return subDirectory;
            }
        }
    }

    public void AddDirectory(Directory directory)
    {
        ArgumentNullException.ThrowIfNull(directory);
        _directories.Add(directory);
    }

    public void AddFile(File file)
    {
        ArgumentNullException.ThrowIfNull(file);
        _files.Add(file);
    }

    public Directory GetDirectory(string name) => _directories.First(d => d.Name == name);

    public ulong CalculateSize()
    {
        var size = 0UL;
        _directories.ForEach(d => size += d.CalculateSize());
        _files.ForEach(f => size += f.Size);
        return size;
    }   

    public IEnumerator<Directory> GetEnumerator()
    {
        yield return this;
        foreach(var directory in IterateDirectories(_directories))
        {
            yield return directory;
        }        
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
