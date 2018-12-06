using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2018.Day5
{
    public class SolutionDay5
    {
        public void RunSolutionPart1()
        {
            var inputFile = File.ReadAllText("Day5/input.txt");
            //var inputFile = "dabAcCaCBAcCcaDA";

            Console.WriteLine($"{Polymerification(inputFile)}");
        }

        private int Polymerification(string input)
        {
            for (var i = 0; i < input.Length - 1;)
            {
                if (Math.Abs(input[i] - input[i + 1]) == ' ') /* 0x20 ( ͡° ͜ʖ ͡°) */
                {
                    input = input.Remove(i, 2);
                    if (i != 0)
                    {
                        i--;
                    }
                }
                else
                {
                    i++;
                }
            }

            return input.Length;
        }

        public void RunSolutionPart2()
        {
            var inputFile = File.ReadAllText("Day5/input.txt");
            //var inputFile = "dabAcCaCBAcCcaDA";
            var lengths = new List<int>();

            for (var i = 65; i < 91; i++)
            {
                var regex = new Regex($"{(char)i}|{((char)i).ToString().ToLower()}");
                var parsedInput = regex.Replace(inputFile, string.Empty);
                lengths.Add(Polymerification(parsedInput));
            }
            Console.WriteLine($"{lengths.Min(c => c)}");
        }
    }
}
