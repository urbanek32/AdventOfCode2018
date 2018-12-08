using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2018.Day7
{
    public class SolutionDay7
    {
        public void RunSolutionPart1()
        {
            var regex = new Regex(@"^Step (?<dependsOn>\w) must be finished before step (?<step>\w) can begin.$");
            var rules = 
                //File.ReadAllLines("Day7/input.txt")
                new List<string>
                {
                    "Step C must be finished before step A can begin.",
                    "Step C must be finished before step F can begin.",
                    "Step A must be finished before step B can begin.",
                    "Step A must be finished before step D can begin.",
                    "Step B must be finished before step E can begin.",
                    "Step D must be finished before step E can begin.",
                    "Step F must be finished before step E can begin.",
                    
                }
                .Select(l => regex.Match(l))
                .Select(m => new
                {
                    Step = m.Groups["step"].Value,
                    DependsOn = m.Groups["dependsOn"].Value
                })
                .ToList();

            var rulesSteps = rules.GroupBy(g => g.Step).ToList();

            var steps = rules
                .Select(r => r.DependsOn)
                .Concat(rules
                    .Select(r => r.Step))
                .Distinct()
                .ToDictionary(s => s, s => false);

            var order = new StringBuilder();

            while (steps.Any(s => !s.Value))
            {
                foreach (var step in steps.Where(s => !s.Value).ToList())
                {
                    var isGood = rulesSteps.All(r => r.Key != step.Key); // check if has any dependencies
                    foreach (var requirement in rulesSteps.Where(r => r.Key == step.Key))
                    {
                        if (steps[requirement.Select(r => r.DependsOn).First()])
                        {
                            isGood = true;
                        }
                        else
                        {
                            isGood = false;
                            break;
                        }
                    }

                    if (isGood)
                    {
                        steps[step.Key] = true;
                        order.Append(step.Key);
                    }
                }
            }

            Console.WriteLine(order);
        }
    }
}
