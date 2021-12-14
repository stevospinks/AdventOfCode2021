using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace Solutions
{
    public class Day14
    {
        private static readonly int day = 14;

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
            var rules = ProcessInput(input, out var template);
            var polymer = GeneratePolymer(rules, template);
            var result = CalculateResult(rules, polymer);
            return result;
        }

        private static int PartTwo(List<string> input)
        {
            return -1;
        }

        private static Dictionary<string, char> ProcessInput(List<string> input, out IList<char> template)
        {
            template = input[0].ToCharArray();

            var rules = new Dictionary<string, char>();
            foreach (var line in input.Skip(1).Except(new List<string> { string.Empty }))
            {
                var rule = line.Split(" -> ");
                rules.Add(rule[0], rule[1].Single());
            }

            return rules;
        }

        private static IList<char> GeneratePolymer(Dictionary<string, char> rules, IList<char> template)
        {
            IList<char> polymer = null;
            for (int step = 0; step < 10; step++)
            {
                if (polymer != null)
                {
                    template = polymer;
                }
                polymer = new List<char>();

                for (int i = 1; i < template.Count; i++)
                {
                    var element1 = template[i - 1];
                    var element2 = template[i];
                    var key = $"{element1}{element2}";

                    var newElement = rules[key];

                    if (i == 1)
                    {
                        polymer.Add(element1);
                    }

                    polymer.Add(newElement);
                    polymer.Add(element2);
                }
            }

            return polymer;
        }

        private static int CalculateResult(Dictionary<string, char> rules, IList<char> polymer)
        {
            var smallestCount = polymer.Count;
            var largestCount = 0;
            foreach (var element in rules.Values.Distinct())
            {
                var elementCount = polymer.Count(c => c == element);

                smallestCount = elementCount < smallestCount ? elementCount : smallestCount;
                largestCount = elementCount > largestCount ? elementCount : largestCount;
            }

            return largestCount - smallestCount;
        }
    }
}
