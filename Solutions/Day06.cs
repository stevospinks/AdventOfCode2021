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
            const int days = 80;

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

        private static long PartTwo(List<int> input)
        {
            const int days = 256;
            const int daysBetweenBirths = 6; // 7 but 0 is counted
            const int daysBeforeFirstBirth = 8; // 9 but 0 is counted

            Dictionary<int, long> lanternfishSchoolYesterday;
            var lanternfishSchoolToday = new Dictionary<int, long>();
            for (int i = 0; i < daysBeforeFirstBirth; i++)
            {
                lanternfishSchoolToday.Add(i, input.Count(val => val == i));
            }

            for (int dayNumber = 0; dayNumber < days; dayNumber++)
            {
                lanternfishSchoolYesterday = new Dictionary<int, long>(lanternfishSchoolToday);

                for (int daysRemainingUntilBirth = lanternfishSchoolYesterday.Keys.Max(); daysRemainingUntilBirth >= 0; daysRemainingUntilBirth--)
                {
                    var lanternfish = lanternfishSchoolYesterday.Single(l => l.Key == daysRemainingUntilBirth);
                    if (lanternfish.Key == 0)
                    {
                        lanternfishSchoolToday[daysBetweenBirths] = lanternfishSchoolToday[daysBetweenBirths] + lanternfish.Value;
                        lanternfishSchoolToday[daysBeforeFirstBirth] = lanternfish.Value;
                    }
                    else
                    {
                        lanternfishSchoolToday[lanternfish.Key - 1] = lanternfish.Value;
                    }
                }

            }

            return lanternfishSchoolToday.Values.Sum();
        }
    }
}
