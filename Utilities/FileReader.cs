using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Utilities
{
    public static class FileReader
    {
        public static IEnumerable<int> ReadInputAsInt(int day, string seperator = "\r\n")
        {
            var lines = ReadInputAsString(day);

            if (seperator != "\r\n")
            {
                lines = lines.Single().Split(seperator);
            }

            var result = lines.Select(l => int.Parse(l));

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
            string root;
            if (Environment.CurrentDirectory.EndsWith(@"\Solutions"))
            {
                // In VS Code
                root = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\"));
            }
            else
            {
                // In Visual Studio
                root = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\"));
            }

            return Path.Combine(root, $@"Input\Day{day:D2}.txt");
        }
    }
}
