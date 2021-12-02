using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace Solutions
{
    public class Day02
    {
        public static void Solve()
        {
            var input = FileReader.ReadInputAsString(2).ToList();

            var partOne = PartOne(input);
            Console.WriteLine($"Day 02, Part One: {partOne}");
        }

        private static int PartOne(List<string> input)
        {
            var forward = 0;
            var down = 0;

            foreach (var instruction in input)
            {
                var instructions = instruction.Split(' ');

                var command = instructions[0];
                var value = Convert.ToInt32(instructions[1]);

                switch (command)
                {
                    case "forward":
                        forward += value;
                        break;
                    case "up":
                        down -= value;
                        break;
                    case "down":
                        down += value;
                        break;
                    default:
                        break;
                }
            }

            return forward * down;
        }
    }
}
