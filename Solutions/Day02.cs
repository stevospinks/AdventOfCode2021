using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace Solutions
{
    public class Day02
    {
        private static readonly int day = 2;

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
            var horizontalPosition = 0;
            var depth = 0;

            foreach (var instruction in input)
            {
                var instructions = instruction.Split(' ');

                var command = instructions[0];
                var value = int.Parse(instructions[1]);

                switch (command)
                {
                    case "forward":
                        horizontalPosition += value;
                        break;
                    case "up":
                        depth -= value;
                        break;
                    case "down":
                        depth += value;
                        break;
                    default:
                        break;
                }
            }

            return horizontalPosition * depth;
        }

        private static int PartTwo(List<string> input)
        {
            var horizontalPosition = 0;
            var depth = 0;
            var aim = 0;

            foreach (var instruction in input)
            {
                var instructions = instruction.Split(' ');

                var command = instructions[0];
                var value = int.Parse(instructions[1]);

                switch (command)
                {
                    case "forward":
                        horizontalPosition += value;
                        depth += value * aim;
                        break;
                    case "up":
                        aim -= value;
                        break;
                    case "down":
                        aim += value;
                        break;
                    default:
                        break;
                }
            }

            return horizontalPosition * depth;
        }
    }
}
