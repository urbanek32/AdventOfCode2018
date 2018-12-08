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
            var mostSleepingGuard = ParseInputShifts()
                .GroupBy(g => g.GuardId)
                .Select(g => new {Group = g, Sum = g.Sum(s => s.Sleeping.Count(c => c == true))}) // match each guard with sum of only sleeping time
                .Aggregate((best, next) => next.Sum > best.Sum ? next : best) // find object with max sum
                .Group;

            var mostSleepingMinute = mostSleepingGuard
                .SelectMany(s => s.Sleeping.Select((sleeping, minute) => new { sleeping, minute })) // have sleeping flag and each minute
                .GroupBy(g => g.minute) // group of each minute containing sleeping flag
                .Select(g => new { Minute = g.Key, Count = g.Count(c => c.sleeping) }) // match each minute with counts
                .Aggregate((best, next) => next.Count > best.Count ? next : best) // find object with max count
                .Minute;

            Console.WriteLine(mostSleepingGuard.Key * mostSleepingMinute);
        }

        private static List<Shift> ParseInputShifts()
        {
            var regex = new Regex(@"^\[(?<timestamp>\d{4}-\d{2}-\d{2} \d{2}:\d{2})\] (?:Guard #(?<guard>\d+) )?(?<event>begins shift|falls asleep|wakes up)$");
            var matches = File.ReadAllLines("Day4/input.txt")
                .OrderBy(l => l)
                .Select(l => regex.Match(l));

            var shifts = new List<Shift>();
            var sleepingMinute = 0;

            foreach (var match in matches)
            {
                switch (match.Groups["event"].Value)
                {
                    case "begins shift":
                        shifts.Add(new Shift(int.Parse(match.Groups["guard"].Value)));
                        break;
                    case "falls asleep":
                        sleepingMinute = DateTime.Parse(match.Groups["timestamp"].Value).Minute;
                        break;
                    case "wakes up":
                        for (var i = sleepingMinute; i < DateTime.Parse(match.Groups["timestamp"].Value).Minute; i++)
                        {
                            shifts.Last().Sleeping[i] = true;
                        }
                        break;
                }
            }

            return shifts;
        }

        private class Shift
        {
            public Shift(int guardId)
            {
                GuardId = guardId;
                Sleeping = new bool[60];
            }

            public int GuardId { get; set; }
            public bool[] Sleeping { get; set; }

        }

        public void RunSolutionPart2()
        {
            var result = ParseInputShifts()
                .GroupBy(g => g.GuardId)
                .Select(g => Enumerable.Range(0, 60) // check each minute for each guard
                    .Select(i => new { Minute = i, Count = g.Count(c => c.Sleeping[i] == true) }) // count if was sleeping on 'i' minute
                    .Aggregate(new {Guard = g.Key, Minute = 0, Count = 0}, // for each guard find minute with has max count
                        (best, next) => next.Count > best.Count
                            ? new {Guard = g.Key, Minute = next.Minute, Count = next.Count}
                            : new {Guard = g.Key, Minute = best.Minute, Count = best.Count}))
                .Aggregate(new { Guard = 0, Minute = 0, Count = 0 },
                    (best, next) => next.Count > best.Count ? next : best);

            Console.WriteLine(result.Guard * result.Minute);
        }
    }
}
