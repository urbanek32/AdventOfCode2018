using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2018.Day8
{
    public class SolutionDay8
    {
        private int[] data;
        private int index;
        private int sum;

        public void RunSolutionPart1()
        {
            data = 
                File.ReadAllText("Day8/input.txt").Trim().Split(' ').Select(int.Parse)
                /*new List<int>
                {
                    2, 3, 0, 3, 10, 11, 12, 1, 1, 0, 1, 99, 2, 1, 1, 2
                }*/
                .ToArray();
        
            ProcessNode();

            Console.WriteLine(sum);
        }

        public void RunSolutionPart2()
        {
            data =
                File.ReadAllText("Day8/input.txt").Trim().Split(' ').Select(int.Parse)
                /*new List<int>
                {
                    2, 3, 0, 3, 10, 11, 12, 1, 1, 0, 1, 99, 2, 1, 1, 2
                }*/
                .ToArray();

            Console.WriteLine(ProcessNode2());
        }
        private int ProcessNode2()
        {
            var childNodesCount = data[index++];
            var metaNodesCount = data[index++];

            var childValues = new List<int>();
            for (var i = 0; i < childNodesCount; i++)
            {
                childValues.Add(ProcessNode2());
            }

            var meta = new List<int>();
            for (var i = 0; i < metaNodesCount; i++)
            {
                meta.Add(data[index++]);
            }

            if (childNodesCount == 0)
            {
                return meta.Sum();
            }

            var nodeValue = 0;
            foreach (var childNode in meta)
            {
                if (childValues.Count > childNode - 1)
                {
                    nodeValue += childValues[childNode - 1];
                }
            }

            return nodeValue;
        }

        private void ProcessNode()
        {
            var childNodes = data[index++];
            var metaNodes = data[index++];

            for (var i = 0; i < childNodes; i++)
            {
                ProcessNode();
            }

            for (var i = 0; i < metaNodes; i++)
            {
                sum += data[index++];
            }
        }
    }
}
