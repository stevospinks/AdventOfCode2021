using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace Solutions
{
    public class Day06
    {
        private static readonly int day = 6;

        public static void Solve()
        {
            var input = FileReader.ReadInputAsInt(day, ",").ToList();

            var partOne = PartOne(input);
            Console.WriteLine($"Day {day:D2}, Part One: {partOne}");

            var partTwo = PartTwo(input);
            Console.WriteLine($"Day {day:D2}, Part Two: {partTwo}");
        }

        private static int PartOne(List<int> input)
        {
            var days = 80;

            var lanternfishSchoolToday = new List<int>(input);
            List<int> lanternfishSchoolYesterday;

            for (int i = 0; i < days; i++)
            {
                lanternfishSchoolYesterday = new List<int>(lanternfishSchoolToday);
                lanternfishSchoolToday.Clear();

                foreach (var lanternfish in lanternfishSchoolYesterday)
                {
                    if (lanternfish == 0)
                    {
                        lanternfishSchoolToday.Add(6);
                        lanternfishSchoolToday.Add(8);
                    }
                    else
                    {
                        lanternfishSchoolToday.Add(lanternfish - 1);
                    }
                }

            }

            return lanternfishSchoolToday.Count;
        }

        private static int PartTwo(List<int> input)
        {
            return -1;
        }
    }
}
