using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018.Day1
{
    public class SolutionDay1
    {
        public void RunSolutionPart1()
        {
            var fileLines = File.ReadAllLines("Day1/input.txt");
            var frequency = fileLines.Sum(line => int.Parse(line));

            Console.WriteLine(frequency);
        }

        public void RunSolutionPart2()
        {
            var fileLines = File.ReadAllLines("Day1/input2.txt");
            var frequency = 0;
            var frequencies = new HashSet<int> {0};

            var done = false;
            do
            {
                foreach (var line in fileLines)
                {
                    frequency += int.Parse(line);
                    if (frequencies.Contains(frequency))
                    {
                        Console.WriteLine("Twice: {0}", frequency);
                        done = true;
                        break;
                    }

                    frequencies.Add(frequency);
                }
            } while (!done);
        }
    }
}
