using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace Solutions
{
    public class Day09
    {
        private static readonly int day = 9;

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
            var heightmap = GenerateHeightmap(input);

            for (int x = 0; x < heightmap.GetLength(0); x++)
            {
                for (int y = 0; y < heightmap.GetLength(1); y++)
                {
                    if (IsLowestPoint(heightmap, x, y, out var height))
                    {
                        var riskLevel = height + 1;
                        result += riskLevel;
                    }
                }
            }

            return result;
        }

        private static int[,] GenerateHeightmap(List<string> input)
        {
            var heightmap = new int[input[0].Length, input.Count];

            for (int y = 0; y < input.Count; y++)
            {
                var line = input[y];
                for (int x = 0; x < line.Length; x++)
                {
                    heightmap[x, y] = (int)char.GetNumericValue(line[x]);
                }
            }

            return heightmap;
        }

        private static bool IsLowestPoint(int[,] heightmap, int x, int y, out int height)
        {
            var maxX = heightmap.GetLength(0) - 1;
            var maxY = heightmap.GetLength(1) - 1;

            height = (int)heightmap.GetValue(x, y);
            var heightNorth = (y - 1 < 0) ? null : (int?)heightmap.GetValue(x, y - 1);
            var heightEast = (x + 1 > maxX) ? null : (int?)heightmap.GetValue(x + 1, y);
            var heightSouth = (y + 1 > maxY) ? null : (int?)heightmap.GetValue(x, y + 1);
            var heightWest = (x - 1 < 0) ? null : (int?)heightmap.GetValue(x - 1, y);

            var isLowestPoint = (!heightNorth.HasValue || (heightNorth.HasValue && heightNorth > height)) &&
                                (!heightEast.HasValue || (heightEast.HasValue && heightEast > height)) &&
                                (!heightSouth.HasValue || (heightSouth.HasValue && heightSouth > height)) &&
                                (!heightWest.HasValue || (heightWest.HasValue && heightWest > height));

            return isLowestPoint;
        }

        private static int PartTwo(List<string> input)
        {
            return -1;
        }
    }
}
