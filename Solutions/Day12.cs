﻿using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace Solutions
{
    public class Day12
    {
        private static readonly int day = 12;

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
            var map = ParseInput(input);
            var paths = BuildPaths(map);
            return paths.Count();
        }

        private static int PartTwo(List<string> input)
        {
            return -1;
        }

        private static Dictionary<string, List<string>> ParseInput(List<string> input)
        {
            var result = new Dictionary<string, List<string>>();

            foreach (var line in input)
            {
                var lineSplit = line.Split('-');
                var source = lineSplit[0];
                var destination = lineSplit[1];

                if (result.TryGetValue(source, out var startValue))
                {
                    startValue.Add(destination);
                }
                else
                {
                    result.Add(source, new List<string> { destination });
                }

                // Add routes in the opposite direction
                if (source != "start" && destination != "end")
                {
                    if (result.TryGetValue(destination, out var endValue))
                    {
                        endValue.Add(source);
                    }
                    else
                    {
                        result.Add(destination, new List<string> { source });
                    }
                }
            }

            return result;
        }

        private static List<List<string>> BuildPaths(Dictionary<string, List<string>> map, List<string> currentPath = null)
        {
            var paths = new List<List<string>>();
            if (currentPath == null)
            {
                currentPath = new List<string> { "start" };
            }

            var searchFrom = currentPath.Last();
            var destinations = map[searchFrom];

            foreach (var destination in destinations)
            {
                var smallCave = destination.ToLowerInvariant() == destination;
                if (smallCave && currentPath.Contains(destination))
                {
                    continue;
                }

                var newPath = new List<string>(currentPath);
                newPath.Add(destination);

                if (destination == "end")
                {
                    paths.Add(newPath);
                    continue;
                }

                paths.AddRange(BuildPaths(map, newPath));
            }

            return paths;
        }
    }
}