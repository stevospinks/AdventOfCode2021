using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace Solutions
{
    public class Day08
    {
        private static readonly int day = 8;

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
            var result = 0;
            foreach (var entry in input)
            {
                var splitEntry = entry.Split(" | ");
                var signalPatterns = splitEntry[0].Split(' ');
                var outputValues = splitEntry[1].Split(' ');

                foreach (var outputValue in outputValues)
                {
                    /*   0:      1:      2:      3:      4:      5:      6:      7:      8:      9:
                        aaaa    ....    aaaa    aaaa    ....    aaaa    aaaa    aaaa    aaaa    aaaa
                       b    c  .    c  .    c  .    c  b    c  b    .  b    .  .    c  b    c  b    c
                       b    c  .    c  .    c  .    c  b    c  b    .  b    .  .    c  b    c  b    c
                        ....    ....    dddd    dddd    dddd    dddd    dddd    ....    dddd    dddd
                       e    f  .    f  e    .  .    f  .    f  .    f  e    f  .    f  e    f  .    f
                       e    f  .    f  e    .  .    f  .    f  .    f  e    f  .    f  e    f  .    f
                        gggg    ....    gggg    gggg    ....    gggg    gggg    ....    gggg    gggg  */
                    switch (outputValue.Length)
                    {
                        case 2: // 1
                            result++;
                            break;
                        case 3: // 7
                            result++;
                            break;
                        case 4: // 4
                            result++;
                            break;
                        case 5: // 2, 3, 5
                            break;
                        case 6: // 0, 6, 9
                            break;
                        case 7: // 8
                            result++;
                            break;
                        default:
                            break;
                    }
                }

            }

            return result;
        }

        private static int PartTwo(List<string> input)
        {
            return -1;
        }
    }
}
