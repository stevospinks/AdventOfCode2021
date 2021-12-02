using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Utilities
{
    public static class FileReader
    {
        public static IEnumerable<int> ReadInputAsInt(int day)
        {
            var lines = ReadInputAsString(day);

            var result = lines.Select(l => Convert.ToInt32(l));

            return result;
        }
        public static IEnumerable<string> ReadInputAsString(int day)
        {
            var path = GetInputPath(day);
            var lines = File.ReadAllLines(path);

            return lines;
        }

        private static string GetInputPath(int day)
        {
            var root = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\"));
            return Path.Combine(root, $@"Input\Day{day:D2}.txt");
        }
    }
}
