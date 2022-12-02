using System.Diagnostics;

namespace AdventOfCode.Utils;

internal static class InputProvider
{
    private static string GetDayForFileName(uint day) => day.ToString().PadLeft(2, '0');
    private static string GetInputFile(uint day) => Path.Combine($"Day{GetDayForFileName(day)}", "input.txt");
    public static IEnumerable<string> Iterate(uint day) => File.ReadAllLines(GetInputFile(day));
}
