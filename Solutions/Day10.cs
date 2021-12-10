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
                        if (characters.TryPop(out char poppedCharacter))
                        {
                            if (GetMatchingOpeningCharacter(character) != poppedCharacter) {
                                // Corrupted line
                                illegalCharacters.Add(character);
                            }
                        }
                        else
                        {
                            // Unfinished line, ignore for now;
                            break;
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

        private static int PartTwo(List<string> input)
        {
            return -1;
        }
    }
}
