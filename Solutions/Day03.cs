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
                var bitsAtPosition = input.Select(binary => binary[i]).ToList();
                if (bitsAtPosition.Count(b => b == '1') > bitsAtPosition.Count / 2)
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
            var oxygenValues = new List<string>(input);
            var co2Values = new List<string>(input);
            for (int i = 0; i < input[0].Length; i++)
            {
                if (oxygenValues.Count() > 1)
                {
                    var mostCommonBit = GetMostCommonBitAtPosition(oxygenValues, i);
                    oxygenValues = oxygenValues.Where(binary => binary[i] == mostCommonBit).ToList();
                }

                if (co2Values.Count() > 1)
                {
                    var leastCommonBit = GetLeastCommonBitAtPosition(co2Values, i);
                    co2Values = co2Values.Where(binary => binary[i] == leastCommonBit).ToList();
                }
            }

            var oxygenGeneratorRating = oxygenValues.Single();
            var co2ScrubberRating = co2Values.Single();

            return Convert.ToInt32(oxygenGeneratorRating, 2) * Convert.ToInt32(co2ScrubberRating, 2);
        }

        private static char GetLeastCommonBitAtPosition(IList<string> input, int position)
        {
            var mostCommonBit = GetMostCommonBitAtPosition(input, position);

            return mostCommonBit == '1' ? '0' : '1';
        }

        private static char GetMostCommonBitAtPosition(IList<string> input, int position)
        {
            var bitsAtPosition = input.Select(i => i[position]).ToList();
            var mostCommonBit = (bitsAtPosition.Count(b => b == '1') >= bitsAtPosition.Count() / 2f) ? '1' : '0';

            return mostCommonBit;
        }
    }
}
