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
        private static readonly char paperMark = '#';
        private static readonly char blankMark = ' ';

        public static void Solve()
        {
            var input = FileReader.ReadInputAsString(day).ToList();

            var partOne = PartOne(input);
            Console.WriteLine($"Day {day:D2}, Part One: {partOne}");

            var partTwo = PartTwo(input);
            Console.WriteLine($"Day {day:D2}, Part Two:\r\n{partTwo}");
        }

        private static int PartOne(List<string> input)
        {
            var startingGrid = ParseInput(input, out var instructions);
            var finalGrid = ProcessInstruction(startingGrid, instructions[0]);
            int result = GetFilledGridCount(finalGrid);

            return result;
        }

        private static string PartTwo(List<string> input)
        {
            var startingGrid = ParseInput(input, out var instructions);
            var finalGrid = ProcessInstructions(startingGrid, instructions);
            var results = GetGridOutputAsString(finalGrid);

            return string.Join("\r\n", results);
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
                grid[coordinate.Item1, coordinate.Item2] = paperMark;
            }

            return grid;
        }

        private static char[,] ProcessInstructions(char[,] startingGrid, List<(string, int)> instructions)
        {
            var grid = startingGrid;
            foreach (var instruction in instructions)
            {
                grid = ProcessInstruction(grid, instruction);
            }

            return grid;
        }


        private static char[,] ProcessInstruction(char[,] grid, (string, int) instruction)
        {
            var xMaxOld = grid.GetLength(0) - 1;
            var yMaxOld = grid.GetLength(1) - 1;

            var foldOnX = instruction.Item1 == "x";
            var foldLocation = instruction.Item2;

            var xMaxNew = foldOnX ? foldLocation - 1 : xMaxOld;
            var yMaxNew = foldOnX ? yMaxOld : foldLocation - 1;
            var newGrid = new char[xMaxNew + 1, yMaxNew + 1];

            for (int x = 0; x <= xMaxOld; x++)
            {
                if (foldOnX && foldLocation == x)
                {
                    continue;
                }

                var newX = x <= xMaxNew ? x : foldLocation - (x - foldLocation);

                for (int y = 0; y <= yMaxOld; y++)
                {
                    if (!foldOnX && foldLocation == y)
                    {
                        continue;
                    }

                    var newY = y <= yMaxNew ? y : foldLocation - (y - foldLocation);

                    if (newGrid[newX, newY] != paperMark)
                    {
                        newGrid[newX, newY] = grid[x, y];
                    }
                }
            }

            return newGrid;
        }

        private static int GetFilledGridCount(char[,] grid)
        {
            var result = 0;
            foreach (var position in grid)
            {
                if (position == paperMark)
                {
                    result++;
                }
            }

            return result;
        }

        private static List<string> GetGridOutputAsString(char[,] grid)
        {
            var lines = new List<string>();
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                var line = string.Empty;
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    var value = grid[x, y];
                    line += value != paperMark ? blankMark : value;
                }

                lines.Add(line);
            }

            return lines;
        }
    }
}
