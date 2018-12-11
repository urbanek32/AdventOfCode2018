using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace AdventOfCode2018.Day10
{
    public class SolutionDay10
    {
        public void RunSolutionPart1()
        {
            var regex = new Regex(@"^position=<\s*(?<posx>-?\d+),\s*(?<posy>-?\d+)> velocity=<\s*(?<velx>-?\d+),\s*(?<vely>-?\d+)>$");
            var coords = 
                File.ReadAllLines("Day10/input.txt")
                /*new List<string>
                {
                    @"position=< 9,  1> velocity=< 0,  2>",
                    @"position=< 7,  0> velocity=<-1,  0>",
                    @"position=< 3, -2> velocity=<-1,  1>",
                    @"position=< 6, 10> velocity=<-2, -1>",
                    @"position=< 2, -4> velocity=< 2,  2>",
                    @"position=<-6, 10> velocity=< 2, -2>",
                    @"position=< 1,  8> velocity=< 1, -1>",
                    @"position=< 1,  7> velocity=< 1,  0>",
                    @"position=<-3, 11> velocity=< 1, -2>",
                    @"position=< 7,  6> velocity=<-1, -1>",
                    @"position=<-2,  3> velocity=< 1,  0>",
                    @"position=<-4,  3> velocity=< 2,  0>",
                    @"position=<10, -3> velocity=<-1,  1>",
                    @"position=< 5, 11> velocity=< 1, -2>",
                    @"position=< 4,  7> velocity=< 0, -1>",
                    @"position=< 8, -2> velocity=< 0,  1>",
                    @"position=<15,  0> velocity=<-2,  0>",
                    @"position=< 1,  6> velocity=< 1,  0>",
                    @"position=< 8,  9> velocity=< 0, -1>",
                    @"position=< 3,  3> velocity=<-1,  1>",
                    @"position=< 0,  5> velocity=< 0, -1>",
                    @"position=<-2,  2> velocity=< 2,  0>",
                    @"position=< 5, -2> velocity=< 1,  2>",
                    @"position=< 1,  4> velocity=< 2,  1>",
                    @"position=<-2,  7> velocity=< 2, -2>",
                    @"position=< 3,  6> velocity=<-1, -1>",
                    @"position=< 5,  0> velocity=< 1,  0>",
                    @"position=<-6,  0> velocity=< 2,  0>",
                    @"position=< 5,  9> velocity=< 1, -2>",
                    @"position=<14,  7> velocity=<-2,  0>",
                    @"position=<-3,  6> velocity=< 2, -1>"
                }*/
                .Select(l => regex.Match(l))
                .Select(m => new Coord
                {
                    PosX = int.Parse(m.Groups["posx"].Value),
                    PosY = int.Parse(m.Groups["posy"].Value),
                    VelX = int.Parse(m.Groups["velx"].Value),
                    VelY = int.Parse(m.Groups["vely"].Value)
                })
                .ToList();

            /*var minX1 = coords.Min(c => c.PosX);
            var minY1 = coords.Min(c => c.PosY);
            foreach (var item in coords)
            {
                item.PosX += Math.Abs(minX1);
                item.PosY += Math.Abs(minY1);
            }*/

            var seconds = 0;

            while (coords.Max(l => l.PosY) - coords.Min(l => l.PosY) > 10)
            {
                foreach (var light in coords)
                {
                    light.PosX += light.VelX;
                    light.PosY += light.VelY;
                }

                seconds++;
            }

            for (var y = coords.Min(l => l.PosY); y <= coords.Max(l => l.PosY); y++)
            {
                var line = new StringBuilder();
                for (var x = coords.Min(l => l.PosX); x <= coords.Max(l => l.PosX); x++)
                {
                    line.Append(coords.Any(l => l.PosX == x && l.PosY == y) ? "#" : ".");
                }
                Console.WriteLine(line);
            }
            Console.WriteLine(seconds);
            /*for (var i = 0; i < 5; i++)
            {
                //Console.WriteLine(i);
                foreach (var item in coords)
                {
                    item.PosX += item.VelX;
                    item.PosY += item.VelY;
                }

                var minX = coords.Min(c => c.PosX);
                var minY = coords.Min(c => c.PosY);
                var maxX = coords.Max(c => c.PosX);
                var maxY = coords.Max(c => c.PosY);

                if (maxY - minY < 20)
                {
                    PrintSky(coords, minX, minY, maxX, maxY);
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                }
            }*/
        }

        private void PrintSky(List<Coord> list, int minX, int minY, int maxX, int maxY)
        {
            for (var y = minY; y <= maxY; y++)
            {
                for (var x = minX; x <= maxX; x++)
                {
                    if (list.Exists(c => c.PosX == x && c.PosY == y))
                    {
                        Console.Write('#');
                    }
                    else
                    {
                        Console.Write('.');
                    }
                }
                Console.WriteLine();
            }
        }

        [DebuggerDisplay("[{PosX}, {PosY}]")]
        private class Coord
        {
            public int PosX { get; set; }
            public int PosY { get; set; }
            public int VelX { get; set; }
            public int VelY { get; set; }
        }
    }
}
