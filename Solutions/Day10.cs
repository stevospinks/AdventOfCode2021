using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace Solutions
{
    public class Day10
    {
        private static readonly int day = 10;

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
            var illegalCharacters = new List<char>();
            foreach (var line in input)
            {
                var characters = new Stack<char>();
                foreach (var character in line)
                {
                    if (IsOpeningCharacter(character))
                    {
                        characters.Push(character);
                    }
                    else
                    {
                        var poppedCharacter = characters.Pop();
                        if (GetMatchingOpeningCharacter(character) != poppedCharacter)
                        {
                            // Corrupted line
                            illegalCharacters.Add(character);
                        }
                    }
                }
            }

            var result = CalculateScore(illegalCharacters);
            return result;
        }

        private static bool IsOpeningCharacter(char character)
        {
            return character == '(' ||
                   character == '[' ||
                   character == '{' ||
                   character == '<';
        }

        private static char GetMatchingOpeningCharacter(char closingCharacter)
        {
            var openingCharacter = closingCharacter switch
            {
                ')' => '(',
                ']' => '[',
                '}' => '{',
                '>' => '<',
                _ => throw new ArgumentException($"'{closingCharacter}' is not a supported character"),
            };

            return openingCharacter;
        }

        private static char GetMatchingClosingCharacter(char openingCharacter)
        {
            var closingCharacter = openingCharacter switch
            {
                '(' => ')',
                '[' => ']',
                '{' => '}',
                '<' => '>',
                _ => throw new ArgumentException($"'{openingCharacter}' is not a supported character"),
            };

            return closingCharacter;
        }

        private static int CalculateScore(List<char> illegalCharacters)
        {
            var score = 0;
            foreach (var character in illegalCharacters)
            {
                score += character switch
                {
                    ')' => 3,
                    ']' => 57,
                    '}' => 1197,
                    '>' => 25137,
                    _ => throw new ArgumentException($"'{character}' is not a supported character"),
                };
            }

            return score;
        }

        private static long CalculateScore(List<string> completionStrings)
        {
            var scores = new List<long>();
            foreach (var completionString in completionStrings)
            {
                long score = 0;
                foreach (var character in completionString)
                {
                    score *= 5;
                    score += character switch
                    {
                        ')' => 1,
                        ']' => 2,
                        '}' => 3,
                        '>' => 4,
                        _ => throw new ArgumentException($"'{character}' is not a supported character"),
                    };
                }

                scores.Add(score);
            }

            scores.Sort();
            var middlePosition = (int)Math.Ceiling(scores.Count / 2f);
            var result = scores.ElementAt(middlePosition - 1);
            return result;
        }

        private static long PartTwo(List<string> input)
        {
            var completionStrings = new List<string>();
            foreach (var line in input)
            {
                var characters = new Stack<char>();
                var corrupted = false;
                for (int i = 0; i < line.Length; i++)
                {
                    var character = line[i];
                    if (IsOpeningCharacter(character))
                    {
                        characters.Push(character);
                    }
                    else
                    {
                        var poppedCharacter = characters.Pop();
                        if (GetMatchingOpeningCharacter(character) != poppedCharacter)
                        {
                            // Corrupted line, ignore
                            corrupted = true;
                            break;
                        }
                    }
                }

                if (!corrupted && characters.Any())
                {
                    // Unfinished line
                    var completionString = string.Empty;
                    foreach (var unfinshedCharacter in characters)
                    {
                        completionString += GetMatchingClosingCharacter(unfinshedCharacter);
                    }

                    completionStrings.Add(completionString);
                }
            }

            var result = CalculateScore(completionStrings);
            return result;
        }
    }
}
