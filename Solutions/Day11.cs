using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace Solutions
{
    public class Day11
    {
        private static readonly int day = 11;

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
            var grid = GenerateGrid(input);

            for (int step = 0; step < 100; step++)
            {
                IncreaseEnergy(grid);

                var flashed = new List<(int, int)>();
                ProcessFlash(grid, flashed);

                result += flashed.Count();
            }

            return result;
        }

        private static int PartTwo(List<string> input)
        {
            return -1;
        }

        private static int[,] GenerateGrid(List<string> input)
        {
            var grid = new int[input[0].Length, input.Count];

            for (int y = 0; y < input.Count; y++)
            {
                var line = input[y];
                for (int x = 0; x < line.Length; x++)
                {
                    grid[x, y] = (int)char.GetNumericValue(line[x]);
                }
            }

            return grid;
        }

        private static void IncreaseEnergy(int[,] grid)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    var energy = (int)grid.GetValue(x, y);
                    grid.SetValue(energy + 1, x, y);
                }
            }
        }

        private static void ProcessFlash(int[,] grid, List<(int, int)> flashed)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    var energy = (int)grid.GetValue(x, y);

                    if (energy > 9)
                    {
                        Flash(grid, x, y, flashed);
                    }
                }
            }
        }

        private static void Flash(int[,] grid, int x, int y, List<(int, int)> flashed)
        {
            grid.SetValue(0, x, y);
            flashed.Add((x, y));

            ProcesssFlashEffect(grid, x - 1, y - 1, flashed); // North West
            ProcesssFlashEffect(grid, x, y - 1, flashed); // North
            ProcesssFlashEffect(grid, x + 1, y - 1, flashed); // North East

            ProcesssFlashEffect(grid, x - 1, y, flashed); // West
            ProcesssFlashEffect(grid, x + 1, y, flashed); // East

            ProcesssFlashEffect(grid, x - 1, y + 1, flashed); // South West
            ProcesssFlashEffect(grid, x, y + 1, flashed); // South
            ProcesssFlashEffect(grid, x + 1, y + 1, flashed); // South East
        }

        private static void ProcesssFlashEffect(int[,] grid, int x, int y, List<(int, int)> flashed)
        {
            var maxX = grid.GetLength(0) - 1;
            var maxY = grid.GetLength(1) - 1;

            if (x < 0 || y < 0 || x > maxX || y > maxY || flashed.Contains((x, y)))
            {
                return;
            }

            var value = (int)grid.GetValue(x, y);
            if (value <= 9)
            {
                value++;
                grid.SetValue(value, x, y);
            }

            if (value > 9)
            {
                Flash(grid, x, y, flashed);
            }
        }
    }
}
