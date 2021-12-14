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
            var polymer = GeneratePolymer(rules, template, 10);
            var result = CalculateResult(polymer);
            return result;
        }

        private static long PartTwo(List<string> input)
        {
            var rules = ProcessInput(input, out var template);

            var expansions = 20; // = 40 / 2 (meaning there will be only two polymer steps required)
            var expandedRules = ExpandRules(rules, expansions);

            // Expand the polymer using one of the remaining two steps
            var polymer = GeneratePolymer(expandedRules, template, 1);

            var result = CalculateResultWithOneRemainingStep(rules, expandedRules, polymer);
            return result;
        }

        private static Dictionary<string, char[]> ProcessInput(List<string> input, out char[] template)
        {
            template = input[0].ToCharArray();

            var rules = new Dictionary<string, char[]>();
            foreach (var line in input.Skip(1).Except(new List<string> { string.Empty }))
            {
                var rule = line.Split(" -> ");
                rules.Add(rule[0], rule[1].ToCharArray());
            }

            return rules;
        }

        private static Dictionary<string, char[]> ExpandRules(Dictionary<string, char[]> rules, int expansionLevels)
        {
            var expandedRules = new Dictionary<string, char[]>();
            foreach (var rule in rules)
            {
                var expandedValue = GeneratePolymer(rules, rule.Key.ToCharArray(), expansionLevels);
                var valueWithoutKey = expandedValue.Skip(1).SkipLast(1).ToArray();
                expandedRules.Add(rule.Key, valueWithoutKey);
            }

            return expandedRules;
        }

        private static char[] GeneratePolymer(Dictionary<string, char[]> rules, char[] template, int steps)
        {
            char[] polymer = null;

            var replacementSize = rules.First().Value.LongLength;
            for (int step = 0; step < steps; step++)
            {
                if (polymer != null)
                {
                    template = polymer;
                }

                long newSize = (template.LongLength * (replacementSize + 1)) - (replacementSize);
                if (newSize > uint.MaxValue)
                {
                    throw new IndexOutOfRangeException($"{newSize} > {uint.MaxValue}!");
                }

                polymer = new char[newSize];
                long polymerIndex = 0;

                for (long i = 1; i < template.LongLength; i++)
                {
                    var element1 = template[i - 1];
                    var element2 = template[i];
                    var key = $"{element1}{element2}";

                    var newElements = rules[key];

                    if (polymerIndex == 0)
                    {
                        polymer[polymerIndex] = element1;
                        polymerIndex++;
                    }

                    foreach (var newElement in newElements)
                    {
                        polymer[polymerIndex] = newElement;
                        polymerIndex++;
                    }

                    polymer[polymerIndex] = element2;
                    polymerIndex++;
                }
            }

            return polymer;
        }

        private static int CalculateResult(char[] polymer)
        {
            var smallestCount = polymer.Length;
            var largestCount = 0;
            foreach (var element in polymer.Distinct())
            {
                var elementCount = polymer.Count(c => c == element);

                smallestCount = elementCount < smallestCount ? elementCount : smallestCount;
                largestCount = elementCount > largestCount ? elementCount : largestCount;
            }

            return largestCount - smallestCount;
        }

        private static long CalculateResultWithOneRemainingStep(Dictionary<string, char[]> initialRules, Dictionary<string, char[]> expandedRules, char[] polymer)
        {
            // Calculate the character counts used for each expansion rule
            var rulesToCountMapping = new Dictionary<string, Dictionary<char, long>>();
            foreach (var rule in expandedRules)
            {
                var newElements = rule.Value;
                var distinctCharacters = newElements.Distinct().ToList();

                var mapping = new Dictionary<char, long>();
                foreach (var character in distinctCharacters)
                {
                    mapping.Add(character, newElements.Count(c => c == character));
                }

                rulesToCountMapping.Add(rule.Key, mapping);
            }

            // Do the final expansion uing the character counts
            var results = initialRules.Select(ir => ir.Value.Single()).Distinct().ToDictionary(e => e, _ => 0L);
            for (long i = 1; i < polymer.LongLength; i++)
            {
                var element1 = polymer[i - 1];
                var element2 = polymer[i];
                var key = $"{element1}{element2}";

                var mapping = rulesToCountMapping[key];
                foreach (var kvp in mapping)
                {
                    results[kvp.Key] += kvp.Value;
                }

                if (i == 1)
                {
                    results[element1]++;
                }

                results[element2]++;
            }

            return results.Values.Max() - results.Values.Min();
        }
    }
}
