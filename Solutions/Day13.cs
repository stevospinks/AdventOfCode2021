using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace Solutions
{
    public class Day13
    {
        private static readonly int day = 13;
        private static readonly string instructionBegins = "fold along ";

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
            var startingGrid = ParseInput(input, out var instructions);
            var finalGrid = ProcessInstruction(startingGrid, instructions[0]);
            int result = ProcessResult(finalGrid);

            return result;
        }

        private static int PartTwo(List<string> input)
        {
            return -1;
        }

        private static char[,] ParseInput(List<string> input, out List<(string, int)> instructions)
        {
            var instructionStrings = input.Where(i => i.StartsWith(instructionBegins));
            var coordinateStrings = input.Except(instructionStrings).Except(new List<string> { string.Empty });

            instructions = instructionStrings.Select(i => i.Replace(instructionBegins, "").Split('=')).Select(isp => (isp[0], int.Parse(isp[1]))).ToList();
            var coordinates = coordinateStrings.Select(c => c.Split(',')).Select(cs => (int.Parse(cs[0]), int.Parse(cs[1]))).ToList();

            var grid = new char[coordinates.Select(c => c.Item1).Max() + 1, coordinates.Select(c => c.Item2).Max() + 1];
            foreach (var coordinate in coordinates)
            {
                grid[coordinate.Item1, coordinate.Item2] = '.';
            }

            return grid;
        }

        private static char[,] ProcessInstruction(char[,] grid, (string, int) instruction)
        {
            var xMaxOld = grid.GetLength(0);
            var yMaxOld = grid.GetLength(1);

            var foldOnX = instruction.Item1 == "x";
            var foldLocation = instruction.Item2;

            var xMaxNew = foldOnX ? foldLocation : xMaxOld;
            var yMaxNew = foldOnX ? yMaxOld : foldLocation;
            var newGrid = new char[xMaxNew + 1, yMaxNew + 1];

            for (int x = 0; x < xMaxOld; x++)
            {
                if (foldOnX && foldLocation == x)
                {
                    continue;
                }

                var newX = x <= xMaxNew ? x : xMaxNew - (x - xMaxNew);

                for (int y = 0; y < yMaxOld; y++)
                {
                    if (!foldOnX && foldLocation == y)
                    {
                        continue;
                    }

                    var newY = y <= yMaxNew ? y : yMaxNew - (y - yMaxNew);

                    if (newGrid[newX, newY] != '.')
                    {
                        newGrid[newX, newY] = grid[x, y];
                    }
                }
            }

            return newGrid;
        }

        private static int ProcessResult(char[,] grid)
        {
            var result = 0;
            foreach (var position in grid)
            {
                if (position == '.')
                {
                    result++;
                }
            }

            return result;
        }
    }
}
