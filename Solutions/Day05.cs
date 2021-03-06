using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace Solutions
{
    public class Day05
    {
        private static readonly int day = 5;

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
            var coverageCoordinates = new Dictionary<Tuple<int, int>, int>();
            foreach (var line in input)
            {
                var coordinateSet = line.Split(" -> ");

                var coordinates1 = coordinateSet[0].Split(',');
                var x1 = int.Parse(coordinates1[0]);
                var y1 = int.Parse(coordinates1[1]);

                var coordinates2 = coordinateSet[1].Split(',');
                var x2 = int.Parse(coordinates2[0]);
                var y2 = int.Parse(coordinates2[1]);

                GenerateLineCoverage(coverageCoordinates, x1, x2, y1, y2, true);
            }

            var result = coverageCoordinates.Count(c => c.Value > 1);
            return result;
        }

        private static void GenerateLineCoverage(Dictionary<Tuple<int, int>, int> coverageCoordinates, int x1, int x2, int y1, int y2, bool ignoreDiagonals)
        {
            if (x1 == x2 || y1 == y2)
            {
                // Process this horizontal or vertical line
                var minX = x1 < x2 ? x1 : x2;
                var maxX = x1 < x2 ? x2 : x1;
                var minY = y1 < y2 ? y1 : y2;
                var maxY = y1 < y2 ? y2 : y1;
                var xEqual = minX == maxX;

                for (int i = (xEqual ? minY : minX); i <= (xEqual ? maxY : maxX); i++)
                {
                    var newCoordinate = xEqual ? new Tuple<int, int>(x1, i) : new Tuple<int, int>(i, y1);
                    if (coverageCoordinates.ContainsKey(newCoordinate))
                    {
                        coverageCoordinates[newCoordinate]++;
                    }
                    else
                    {
                        coverageCoordinates.Add(newCoordinate, 1);
                    }
                }
            }
            else if (!ignoreDiagonals)
            {
                // Process this diagonal line
                var xIncreasing = x1 < x2;
                var yIncreasing = y1 < y2;
                while (xIncreasing ? x1 <= x2 : x1 >= x2)
                {
                    var newCoordinate = new Tuple<int, int>(x1, y1);
                    if (coverageCoordinates.ContainsKey(newCoordinate))
                    {
                        coverageCoordinates[newCoordinate]++;
                    }
                    else
                    {
                        coverageCoordinates.Add(newCoordinate, 1);
                    }

                    x1 = xIncreasing ? x1 + 1 : x1 - 1;
                    y1 = yIncreasing ? y1 + 1 : y1 - 1;
                }
            }
        }

        private static int PartTwo(List<string> input)
        {
            var coverageCoordinates = new Dictionary<Tuple<int, int>, int>();
            foreach (var line in input)
            {
                var coordinateSet = line.Split(" -> ");

                var coordinates1 = coordinateSet[0].Split(',');
                var x1 = int.Parse(coordinates1[0]);
                var y1 = int.Parse(coordinates1[1]);

                var coordinates2 = coordinateSet[1].Split(',');
                var x2 = int.Parse(coordinates2[0]);
                var y2 = int.Parse(coordinates2[1]);

                GenerateLineCoverage(coverageCoordinates, x1, x2, y1, y2, false);
            }

            var result = coverageCoordinates.Count(c => c.Value > 1);
            return result;
        }
    }
}
