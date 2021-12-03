using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace Solutions
{
    public class Day03
    {
        private static readonly int day = 3;

        public static void Solve()
        {
            var input = FileReader.ReadInputAsString(day).ToList();

            var partOne = PartOne(input);
            Console.WriteLine($"Day {day:D2}, Part One: {partOne}");

            var partTwo = PartTwo(input);
            Console.WriteLine($"Day {day:D2}, Part Two: {partTwo}");
        }

        private static int PartOne(List<string> input)
        {
            var gammaRate = string.Empty;
            var epsilonRate = string.Empty;

            for (int i = 0; i < input[0].Length; i++)
            {
                var bitsAtPosition = input.Select(binary => char.GetNumericValue(binary[i])).ToList();
                if (bitsAtPosition.Count(b => b == 1) > bitsAtPosition.Count / 2)
                {
                    gammaRate += '1';
                    epsilonRate += '0';
                }
                else
                {
                    gammaRate += '0';
                    epsilonRate += '1';
                }
            }

            return Convert.ToInt32(gammaRate, 2) * Convert.ToInt32(epsilonRate, 2);
        }

        private static int PartTwo(List<string> input)
        {
            return 0;
        }
    }
}
