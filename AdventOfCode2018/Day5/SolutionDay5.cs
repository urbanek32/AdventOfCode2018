using System;
using System.IO;

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

        }
    }
}
