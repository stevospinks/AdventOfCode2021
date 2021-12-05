using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace Solutions
{
    public class Day04
    {
        private static readonly int day = 4;

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
            var numbersCalled = input[0].Split(',').Select(n => int.Parse(n));
            var boards = ParseBoards(input);

            foreach (var numberCalled in numbersCalled)
            {
                foreach (var board in boards)
                {
                    for (int i = 0; i < board.GetLength(0); i++)
                    {
                        var marked = false;
                        for (int j = 0; j < board.GetLength(1); j++)
                        {
                            if (board[i, j].Item1 == numberCalled)
                            {
                                board[i, j] = new Tuple<int, bool>(board[i, j].Item1, true);
                                marked = true;

                                var fullRow = Enumerable.Range(0, board.GetLength(0)).Select(r => board[i, r]).All(v => v.Item2);
                                if (fullRow)
                                {
                                    return ProcessWin(numberCalled, board);
                                }
                                else
                                {
                                    var fullColumn = Enumerable.Range(0, board.GetLength(1)).Select(c => board[c, j]).All(v => v.Item2);
                                    if (fullColumn)
                                    {
                                        return ProcessWin(numberCalled, board);
                                    }
                                }
                                break;
                            }

                            if (marked)
                            {
                                break;
                            }
                        }

                        if (marked)
                        {
                            marked = false;
                            break;
                        }
                    }
                }
            }

            return -1;
        }

        private static List<Tuple<int, bool>[,]> ParseBoards(List<string> input)
        {
            var boards = new List<Tuple<int, bool>[,]>();
            var board = new Tuple<int, bool>[5, 5];
            var boardCount = 0;
            for (int i = 2; i < input.Count; i++)
            {
                var line = input[i];
                if (line.Trim() == string.Empty)
                {
                    boards.Add(board);
                    board = new Tuple<int, bool>[5, 5];
                    boardCount = 0;
                }
                else
                {
                    var values = line.Split(' ').Where(s => s != string.Empty).Select(s => new Tuple<int, bool>(int.Parse(s), false)).ToList();
                    board[boardCount, 0] = values[0];
                    board[boardCount, 1] = values[1];
                    board[boardCount, 2] = values[2];
                    board[boardCount, 3] = values[3];
                    board[boardCount, 4] = values[4];
                    boardCount++;
                }
            }

            return boards;
        }

        private static int ProcessWin(int numberCalled, Tuple<int, bool>[,] board)
        {
            var unmarkedTotal = 0;
            foreach (var item in board)
            {
                if (!item.Item2)
                {
                    unmarkedTotal += item.Item1;
                }
            }

            return unmarkedTotal * numberCalled;
        }

        private static int PartTwo(List<string> input)
        {
            return 0;
        }
    }
}
