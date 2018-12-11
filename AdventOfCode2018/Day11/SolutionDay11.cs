using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace AdventOfCode2018.Day11
{
    public class SolutionDay11
    {
        private const int GridSerialNumber = 18;
        private int[,] Grid = new int[301, 301];
        
        public void RunSolutionPart1()
        {
            var maxPower = 0;
            var maxX = 1;
            var maxY = 1;
            for (var x = 1; x <= 300; x++)
            {
                for (var y = 1; y <= 300; y++)
                {
                    var sum = CalculateSquarePower(x, y, 3, 3);
                    if (sum > maxPower)
                    {
                        maxPower = sum;
                        maxX = x;
                        maxY = y;
                    }

                    Grid[x, y] = sum;
                }
            }

            Console.WriteLine($"({maxX}, {maxY}) = {maxPower}");
        }

        public void RunSolutionPart2()
        {
            RunSolutionPart1();

            var maxes = new[] {
                new { maxPower = 0, maxX = 0, maxY = 0, maxS = 0 }
            }.ToList();

            Parallel.ForEach(Enumerable.Range(1, 300), s =>
            {
                var maxPower = 0;
                var maxX = 1;
                var maxY = 1;
                var maxS = 1;

                for (var x = 1; x <= 300 - s; x++)
                {
                    for (var y = 1; y <= 300 - s; y++)
                    {
                        Console.WriteLine($"x: {x} y: {y} s: {s} {Thread.CurrentThread.ManagedThreadId}");
                        var sum = GetSquarePower(x, y, s, s);
                        if (sum > maxPower)
                        {
                            maxPower = sum;
                            maxX = x;
                            maxY = y;
                            maxS = s;
                        }
                    }
                }

                maxes.Add(new {maxPower = maxPower, maxX = maxX, maxY = maxY, maxS = maxS});
            });

            var result = maxes.Aggregate(new { maxPower = 0, maxX = 0, maxY = 0, maxS = 0 },
                (best, next) => best.maxPower > next.maxPower ? best : next);

            Console.WriteLine($"({result.maxX}, {result.maxY}, {result.maxS}) = {result.maxPower}");

            /*for (var s = 1; s <= 300; s++)
            {
                for (var x = 1; x <= 300 - s; x++)
                {
                    for (var y = 1; y <= 300 - s; y++)
                    {
                        //Console.WriteLine($"{x} {y} {s}");
                        var sum = GetSquarePower(x, y, s, s);
                        if (sum > maxPower)
                        {
                            maxPower = sum;
                            maxX = x;
                            maxY = y;
                            maxS = s;
                        }
                    }
                }
            }*/

            //Console.WriteLine($"({maxX}, {maxY}, {maxS}) = {maxPower}");
        }
        private int GetSquarePower(int x, int y, int sizeX, int sizeY)
        {
            var sum = 0;
            for (var i = x; i < x + sizeX; i++)
            {
                for (var j = y; j < y + sizeY; j++)
                {
                    sum += Grid[i, j];
                }
            }

            return sum;
        }

        private static int CalculateSquarePower(int x, int y, int sizeX, int sizeY)
        {
            var sum = 0;
            for (var i = x; i < x + sizeX; i++)
            {
                for (var j = y; j < y + sizeY; j++)
                {
                    sum += CalculatePowerLevel(i, j);
                }
            }

            return sum;
        }

        private static int CalculatePowerLevel(int x, int y)
        {
            var powerLevel = 0;
            var rackId = x + 10;
            powerLevel =  rackId * y;
            powerLevel += GridSerialNumber;
            powerLevel *= rackId;
            powerLevel = Math.Abs(powerLevel/100%10);
            powerLevel -= 5;

            return powerLevel;
        }
    }
}
