namespace AdventOfCode.Utils;

internal static class InputProvider
{
    private static string GetDayFileName(uint day) => $"Day{day.ToString().PadLeft(2, '0')}";
    private static string GetInputFile(uint day) => Path.Combine(GetDayFileName(day), "input.txt");
    private static string GetSampleFile(uint day) => Path.Combine(GetDayFileName(day), "sample.txt");
    public static IEnumerable<string> Iterate(uint day) => File.ReadLines(GetInputFile(day));
    public static IEnumerable<string> IterateSample(uint day) => File.ReadLines(GetSampleFile(day));
}
