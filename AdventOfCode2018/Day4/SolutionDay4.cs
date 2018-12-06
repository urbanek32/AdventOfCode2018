using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2018.Day4
{
    public class SolutionDay4
    {
        public void RunSolutionPart1()
        {
            var regex = new Regex(@"^\[(?<timestamp>\d{4}-\d{2}-\d{2} \d{2}:\d{2})\] (?:Guard #(?<guard>\d+) )?(?<event>begins shift|falls asleep|wakes up)$");
            var maches = File.ReadAllLines("Day4/input.txt")
                .OrderBy(l => l)
                .Select(l => regex.Match(l));

            var shifts = new List<Shift>();
            foreach (var match in maches)
            {

            }
        }
        private class Shift
        {
            public int GuardId { get; set; }
            public bool[] Sleeping { get; set; }

        }

        public void RunSolutionPart2()
        {
            
        }
    }
}
