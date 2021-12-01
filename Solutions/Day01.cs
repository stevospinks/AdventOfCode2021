using System;
using System.Linq;
using Utilities;

namespace Solutions
{
    public class Day01
    {
        public static void Main(string[] args)
        {
            var input = FileReader.ReadInputAsInt(1).ToList();

            var result = 0;
            // Ignore the first input, nothing to compare it to
            for (int i = 1; i < input.Count(); i++)
            {
                if (input[i] > input[i-1])
                {
                    result++;
                }
            }

            Console.WriteLine(result);
        }
    }
}
