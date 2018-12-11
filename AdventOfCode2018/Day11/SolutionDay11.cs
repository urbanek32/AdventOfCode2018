using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2018.Day11
{
    public class SolutionDay11
    {
        private const int GridSerialNumber = 7400;
        
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
                }
            }

            Console.WriteLine($"Part1: ({maxX}, {maxY}) = {maxPower}");
        }

        public void RunSolutionPart2()
        {
            var maxPower = 0;
            var maxX = 1;
            var maxY = 1;
            var maxS = 1;

            for (var x = 1; x <= 300; x++)
            {
                for (var y = 1; y <= 300; y++)
                {
                    foreach (var size in Enumerable.Range(1, Math.Min(20, 300 - Math.Max(x, y))))
                    {
                        //Console.WriteLine($"x:{x} y:{y} s:{size}");
                        var sum = CalculateSquarePower(x, y, size, size);
                        if (sum > maxPower)
                        {
                            maxPower = sum;
                            maxX = x;
                            maxY = y;
                            maxS = size;
                        }
                    }
                }
            }

            Console.WriteLine($"Part2: ({maxX}, {maxY}, {maxS}) = {maxPower}");
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
