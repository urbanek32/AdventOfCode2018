using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode2018.Day3
{
    public class SolutionDay3
    {
        public void RunSolutionPart1()
        {
            var fileLines = File.ReadAllLines("Day3/input.txt");
            /*var fileLines = new List<string>
            {
                "#1 @ 1,3: 4x4",
                "#2 @ 3,1: 4x4",
                "#3 @ 5,5: 2x2"
            };*/
            var coords = new int[1000, 1000];
            var seen = new bool[1000, 1000];
            var conflicts = 0;

            foreach (var line in fileLines)
            {
                var input = ParseInput(line);
                Console.WriteLine($"{input.Id} / 1360");
                for (var i = input.X; i < input.X + input.Width; i++)
                {
                    for (var j = input.Y; j < input.Y + input.Height; j++)
                    {
                        coords[i, j]++;
                        if (coords[i, j] > 1)
                        {
                            seen[i, j] = true;
                        }
                    }
                }
            }

            for (var i = 0; i < 1000; i++)
            {
                for (var j = 0; j < 1000; j++)
                {
                    if (seen[i, j])
                    {
                        conflicts++;
                    }
                }
            }

            Console.WriteLine($"Conflicts: {conflicts}");
        }

        public void RunSolutionPart2()
        {
            var fileLines = File.ReadAllLines("Day3/input.txt");
            /*var fileLines = new List<string>
            {
                "#1 @ 1,3: 4x4",
                "#2 @ 3,1: 4x4",
                "#3 @ 5,5: 2x2"
            };*/
            var coords = new int[1000, 1000];
            var vals = new Dictionary<int, Rect>();
            
            foreach (var line in fileLines)
            {
                var input = ParseInput(line);
                vals.Add(input.Id, input);

                Console.WriteLine($"{input.Id} / 1360");
                for (var i = input.X; i < input.X + input.Width; i++)
                {
                    for (var j = input.Y; j < input.Y + input.Height; j++)
                    {
                        coords[i, j]++;
                    }
                }
            }

            foreach (var val in vals)
            {
                var value = val.Value;
                var overlaps = 0;
                for (var i = value.X; i < value.X + value.Width; i++)
                {
                    for (var j = value.Y; j < value.Y + value.Height; j++)
                    {
                        if (coords[i, j] != 1)
                        {
                            overlaps++;
                        }
                    }
                }

                if (overlaps == 0)
                {
                    Console.WriteLine($"ID: {val.Key}");
                }
            }
        }

        private static Rect ParseInput(string line)
        {
            const string pattern = @"#(\d+) @ (\d+),(\d+): (\d+)x(\d+)";
            var result = Regex.Match(line, pattern);

            return new Rect
            {
                Id = int.Parse(result.Groups[1].Value),
                X = int.Parse(result.Groups[2].Value),
                Y = int.Parse(result.Groups[3].Value),
                Width = int.Parse(result.Groups[4].Value),
                Height = int.Parse(result.Groups[5].Value)
            };
        }

        private class Rect
        {
            public int Id { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
        }
    }
}
