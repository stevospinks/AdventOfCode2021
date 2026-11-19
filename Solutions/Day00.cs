using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace Solutions
{
    public class Day00
    {
        private static readonly int day = 0;

        public static void Solve()
        {
            var input = FileReader.ReadInputAsInt(day).ToList();

            var partOne = PartOne(input);
            Console.WriteLine($"Day {day:D2}, Part One: {partOne}");

            var partTwo = PartTwo(input);
            Console.WriteLine($"Day {day:D2}, Part Two: {partTwo}");
        }

        private static int PartOne(List<int> input)
        {
            var result = 0;

            return result;
        }

        private static int PartTwo(List<int> input)
        {
            var result = 0;

            return result;
        }
    }
}
