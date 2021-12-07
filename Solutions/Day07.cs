using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace Solutions
{
    public class Day07
    {
        private static readonly int day = 7;

        public static void Solve()
        {
            var input = FileReader.ReadInputAsInt(day, ",").ToList();

            var partOne = PartOne(input);
            Console.WriteLine($"Day {day:D2}, Part One: {partOne}");

            var partTwo = PartTwo(input);
            Console.WriteLine($"Day {day:D2}, Part Two: {partTwo}");
        }

        private static int PartOne(List<int> input)
        {
            input.Sort();
            var median = input[input.Count / 2];

            var result = input.Sum(i => i <= median ? median - i : i - median);
            return result;
        }

        private static int PartTwo(List<int> input)
        {
            var mean = input.Sum() / (decimal)input.Count;
            var meanRoundedUp = (int)Math.Round(mean, MidpointRounding.AwayFromZero);
            var meanRoundedDown = (int)Math.Round(mean, MidpointRounding.ToZero);

            var resultFromMeanRoundedUp = input.Sum(i => i <= meanRoundedUp ? CalculateFuelUsage(meanRoundedUp - i) : CalculateFuelUsage(i - meanRoundedUp));
            var resultFromMeanRoundedDown = input.Sum(i => i <= meanRoundedDown ? CalculateFuelUsage(meanRoundedDown - i) : CalculateFuelUsage(i - meanRoundedDown));

            return resultFromMeanRoundedUp < resultFromMeanRoundedDown ? resultFromMeanRoundedUp : resultFromMeanRoundedDown;
        }

        private static int CalculateFuelUsage(decimal distance)
        {
            var result = 0;

            for (int i = 1; i <= distance; i++)
            {
                result += (i);
            }

            return result;
        }
    }
}
