using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018.Day2
{
    public class SolutionDay2
    {
        public void RunSolutionPart1()
        {
            var fileLines = File.ReadAllLines("Day2/input.txt");
            var threeTimes = 0;
            var twoTimes = 0;

            foreach (var line in fileLines)
            {
                var letters = new Dictionary<char, int>();
                foreach (var letter in line)
                {
                    if (letters.TryGetValue(letter, out _))
                    {
                        letters[letter]++;
                    }
                    else
                    {
                        letters.Add(letter, 1);
                    }
                }

                if (letters.ContainsValue(3))
                {
                    threeTimes++;
                }

                if (letters.ContainsValue(2))
                {
                    twoTimes++;
                }
            }

            Console.WriteLine($"Sum: {threeTimes * twoTimes}");
        }

        public void RunSolutionPart2()
        {
            var fileLines = File.ReadAllLines("Day2/input.txt");

            foreach (var lineA in fileLines)
            {
                foreach (var lineB in fileLines)
                {
                    if (lineA == lineB) continue;

                    var diffs = 0;
                    var diff_idx = -1;
                    for (var i = 0; i < lineB.Length; i++)
                    {
                        if (lineA[i] != lineB[i])
                        {
                            diffs++;
                            diff_idx = i;
                        }
                    }

                    if (diffs == 1)
                    {
                        Console.WriteLine($"{lineA.Substring(0, diff_idx)}{lineA.Substring(diff_idx+1)}");
                        return;
                    }
                }
            }
        }
    }
}
