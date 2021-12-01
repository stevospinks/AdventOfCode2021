using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace Solutions
{
    public class Day01
    {
        public static void Main(string[] args)
        {
            var input = FileReader.ReadInputAsInt(1).ToList();

            var partOne = PartOne(input);
            Console.WriteLine($"Part One: {partOne}");

            var partTwo = PartTwo(input);
            Console.WriteLine($"Part Two: {partTwo}");
        }

        private static int PartOne(List<int> input)
        {
            var result = 0;

            // Ignore the first input, nothing to compare it to
            for (int i = 1; i < input.Count(); i++)
            {
                if (input[i] > input[i - 1])
                {
                    result++;
                }
            }

            return result;
        }

        private static int PartTwo(List<int> input)
        {
            var result = 0;

            var measurementWindowSums = new List<int>();
            // Ignore the first two inputs, cannot create window
            for (int i = 2; i < input.Count(); i++)
            {
                measurementWindowSums.Add(input[i] + input[i - 1] + input[i - 2]);
            }

            // Ignore the first input, nothing to compare it to
            for (int i = 1; i < measurementWindowSums.Count(); i++)
            {
                if (measurementWindowSums[i] > measurementWindowSums[i - 1])
                {
                    result++;
                }
            }

            return result;
        }
    }
}
