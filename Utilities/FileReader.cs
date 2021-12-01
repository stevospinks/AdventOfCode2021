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
            var path = GetInputPath(day);
            var lines = File.ReadAllLines(path);

            var result = lines.Select(l => Convert.ToInt32(l));

            return result;
        }

        private static string GetInputPath(int day)
        {
            var root = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\"));
            return Path.Combine(root, $@"Input\Day{day.ToString("D2")}.txt");
        }
    }
}
