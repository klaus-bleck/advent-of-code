using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day01;

internal static class InputProvider
{

    private static readonly string InputFile = Path.Combine("Day01", "input.txt");

    public static IEnumerable<string> Iterate() => File.ReadAllLines(InputFile);
}
