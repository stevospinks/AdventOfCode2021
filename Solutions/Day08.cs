using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace Solutions
{
    public class Day08
    {
        private static readonly int day = 8;

        public static void Solve()
        {
            var input = FileReader.ReadInputAsString(day).ToList();

            var partOne = PartOne(input);
            Console.WriteLine($"Day {day:D2}, Part One: {partOne}");

            var partTwo = PartTwo(input);
            Console.WriteLine($"Day {day:D2}, Part Two: {partTwo}");
        }

        /*   0:      1:      2:      3:      4:      5:      6:      7:      8:      9:
            aaaa    ....    aaaa    aaaa    ....    aaaa    aaaa    aaaa    aaaa    aaaa
           b    c  .    c  .    c  .    c  b    c  b    .  b    .  .    c  b    c  b    c
           b    c  .    c  .    c  .    c  b    c  b    .  b    .  .    c  b    c  b    c
            ....    ....    dddd    dddd    dddd    dddd    dddd    ....    dddd    dddd
           e    f  .    f  e    .  .    f  .    f  .    f  e    f  .    f  e    f  .    f
           e    f  .    f  e    .  .    f  .    f  .    f  e    f  .    f  e    f  .    f
            gggg    ....    gggg    gggg    ....    gggg    gggg    ....    gggg    gggg
             =6      =2      =5      =5      =4      =5      =6      =3      =7      =6   */

        private static int PartOne(List<string> input)
        {
            var result = 0;
            foreach (var entry in input)
            {
                var splitEntry = entry.Split(" | ");
                var signalPatterns = splitEntry[0].Split(' ');
                var outputValues = splitEntry[1].Split(' ');

                foreach (var outputValue in outputValues)
                {
                    switch (outputValue.Length)
                    {
                        case 2: // 1
                            result++;
                            break;
                        case 3: // 7
                            result++;
                            break;
                        case 4: // 4
                            result++;
                            break;
                        case 5: // 2, 3, 5
                            break;
                        case 6: // 0, 6, 9
                            break;
                        case 7: // 8
                            result++;
                            break;
                        default:
                            break;
                    }
                }

            }

            return result;
        }

        private static int PartTwo(List<string> input)
        {
            var mappingSegmentStringToValue = new Dictionary<string, string>
            {
                { "abcefg", "0" },
                { "cf", "1" },
                { "acdeg", "2" },
                { "acdfg", "3" },
                { "bcdf", "4" },
                { "abdfg", "5" },
                { "abdefg", "6" },
                { "acf", "7" },
                { "abcdefg", "8" },
                { "abcdfg", "9" }
            };

            var result = 0;
            foreach (var entry in input)
            {
                var splitEntry = entry.Split(" | ");

                var signalPatterns = splitEntry[0].Split(' ');
                var signalPatternsOrdered = signalPatterns.Select(sp => SortStringAlphabetically(sp)).ToList();

                var outputValues = splitEntry[1].Split(' ');
                var outputValuesOrdered = outputValues.Select(sp => SortStringAlphabetically(sp)).ToList();
                var mappingSegmentToSegment = CalculateSegmentToSegmentMapping(signalPatternsOrdered);

                var outputValueTranslated = string.Empty;
                foreach (var outputValue in outputValuesOrdered)
                {
                    var translatedOutput = new List<char>();

                    foreach (var outputValueCharacter in outputValue)
                    {
                        translatedOutput.Add(mappingSegmentToSegment.Single(m => m.Value == outputValueCharacter).Key);
                    }

                    var key = SortStringAlphabetically(translatedOutput);
                    outputValueTranslated += mappingSegmentStringToValue[key];
                }

                var outputValueAsNumber = int.Parse(outputValueTranslated);
                result += outputValueAsNumber;
            }

            return result;
        }

        private static Dictionary<char, char> CalculateSegmentToSegmentMapping(List<string> signalPatternsOrdered)
        {
            var mappingValueToSegmentCount = new Dictionary<int, int>
            {
                { 0, 6 },
                { 1, 2 },
                { 2, 5 },
                { 3, 5 },
                { 4, 4 },
                { 5, 5 },
                { 6, 6 },
                { 7, 3 },
                { 8, 7 },
                { 9, 6 }
            };
            var mappingSegmentToSegment = new Dictionary<char, char> ();
            
            // a = difference between 1 and 7
            mappingSegmentToSegment.Add('a', signalPatternsOrdered.Single(o => o.Length == mappingValueToSegmentCount[7])
                .Except(signalPatternsOrdered.Single(o => o.Length == mappingValueToSegmentCount[1])).Single());
            
            // c = value from 1 that is only in 2 of the three 6-segment displays
            var valuesToCheck = signalPatternsOrdered.Single(o => o.Length == mappingValueToSegmentCount[1]);
            mappingSegmentToSegment.Add('c', signalPatternsOrdered.Where(s => s.Length == 6).Count(s => s.Contains(valuesToCheck[0])) == 2 ? valuesToCheck[0] : valuesToCheck[1]);
            
            // e = difference between 8 and 9 (9 = only 5-segment display without c) + c
            var valueForNine = SortStringAlphabetically(signalPatternsOrdered.Where(s => s.Length == 5 && !s.Contains(mappingSegmentToSegment['c'])).Single() + mappingSegmentToSegment['c']);
            mappingSegmentToSegment.Add('e', signalPatternsOrdered.Single(o => o.Length == mappingValueToSegmentCount[8]).Except(valueForNine).Single());
            
            // f = 7 - a,c
            var ac = new List<char> { mappingSegmentToSegment['a'], mappingSegmentToSegment['c'] };
            mappingSegmentToSegment.Add('f', signalPatternsOrdered.Single(o => o.Length == mappingValueToSegmentCount[7]).Except(ac).Single());
            
            // d = difference between 8 and 0 (0 = only 6-segment display containing a, c, e, f)
            var acef = new List<char> { mappingSegmentToSegment['a'], mappingSegmentToSegment['c'], mappingSegmentToSegment['e'], mappingSegmentToSegment['f'] };
            var valueForZero = SortStringAlphabetically(signalPatternsOrdered.Where(s => s.Length == 6 && s.Except(acef).Count() == 2).Single());
            mappingSegmentToSegment.Add('d', signalPatternsOrdered.Single(o => o.Length == mappingValueToSegmentCount[8]).Except(valueForZero).Single());
            
            // b = 4 - c,d,f
            var cdf = new List<char> { mappingSegmentToSegment['c'], mappingSegmentToSegment['d'], mappingSegmentToSegment['f'] };
            mappingSegmentToSegment.Add('b', signalPatternsOrdered.Single(o => o.Length == mappingValueToSegmentCount[4]).Except(cdf).Single());
            
            // g = 8 - all other values
            var allOtherValues = mappingSegmentToSegment.Values;
            mappingSegmentToSegment.Add('g', signalPatternsOrdered.Single(o => o.Length == mappingValueToSegmentCount[8]).Except(allOtherValues).Single());

            return mappingSegmentToSegment;
        }

        private static string SortStringAlphabetically(string value)
        {
            return string.Join("", value.OrderBy(character => character));
        }

        private static string SortStringAlphabetically(IEnumerable<char> value)
        {
            return string.Join("", value.OrderBy(character => character));
        }
    }
}
