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
                            conflicts++;
                        }
                    }
                }
            }

            Console.WriteLine($"Conflicts: {conflicts}");
            File.WriteAllText("out.txt", $"{conflicts}");
        }

        public void RunSolutionPart2()
        {

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

        private class TupleComparer : IEqualityComparer<Tuple<int, int>>
        {
            public bool Equals(Tuple<int, int> t1, Tuple<int, int> t2)
            {
                return t1.Item1.Equals(t2.Item1) && t1.Item2.Equals(t2.Item2);
            }

            public int GetHashCode(Tuple<int, int> obj)
            {
                return base.GetHashCode();
            }
        }
    }
}
