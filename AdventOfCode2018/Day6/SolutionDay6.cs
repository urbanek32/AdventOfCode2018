using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2018.Day6
{
    public class SolutionDay6
    {
        public void RunSolutionPart1()
        {
            var coords = File.ReadAllLines("Day6/input.txt")
                .Select(s => s.Split(", "))
                .Select(s => s.Select(i => Convert.ToInt32(i)).ToArray())
                .Select(s => (x: s[0], y: s[1]))
                .ToArray();

            /*var coords = new (int x, int y)[]
            {
                (1, 1),
                (1, 6),
                (8, 3),
                (3, 4),
                (5, 5),
                (8, 9)
            };*/

            var maxX = coords.Max(c => c.x);
            var maxY = coords.Max(c => c.y);

            var grid = new int[maxX + 2, maxY + 2];
            var safeCount = 0;

            for (var x = 0; x <= maxX + 1; x++)
            {
                for (var y = 0; y <= maxY + 1; y++)
                {
                    var distances = coords
                        .Select((c, i) => (i: i, dist: Distance(x, y, c.x, c.y)))
                        .OrderBy(c => c.dist)
                        .ToArray();

                    if (distances[1].dist != distances[0].dist)
                    {
                        grid[x, y] = distances[0].i;
                    }
                    else
                    {
                        grid[x, y] = -1;
                    }

                    if (distances.Sum(c => c.dist) < 10000)
                    {
                        safeCount++;
                    }
                }
            }

            var excluded = new List<int>();
            var counts = Enumerable.Range(-1, coords.Length + 1).ToDictionary(i => i, _ => 0);
            for (var x = 0; x <= maxX + 1; x++)
            {
                for (var y = 0; y <= maxY + 1; y++)
                {
                    if (x == 0 || y == 0 || x == maxX + 1 || y == maxY + 1)
                    {
                        excluded.Add(grid[x, y]);
                    }

                    counts[grid[x, y]] += 1;
                }
            }

            excluded = excluded.Distinct().ToList();
            var result = counts
                .Where(k => !excluded.Contains(k.Key))
                .OrderByDescending(k => k.Value)
                .First();

            Console.WriteLine($"PartA:{result.Value}");
            Console.WriteLine($"{safeCount}");
        }

        private static int Distance(int x, int y, int pointX, int pointY)
        {
            return Math.Abs(x - pointX) + Math.Abs(y - pointY);
        }
    }
}
