using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01_SonarSweep.Services
{
    internal static class InputDataHandler
    {
        //public static (int Steps, List<int> inputData) ReadDataFromFile()
        //{
        //    Console.Write("Read data from example: ");
        //    var exampleNr = Console.ReadLine();

        //    Console.Write("Calculate after steps: ");
        //    var stepsStr = Console.ReadLine();

        //    var content = File.ReadAllLines($"Resources\\Example{exampleNr}.txt");
        //    var inputData = content.Select(x => int.Parse(x)).ToList();

        //    var steps = int.Parse(stepsStr);

        //    return (steps, inputData);
        //}

        public static List<int> ReadDataFromFile()
        {
            Console.Write("Read data from example: ");
            var exampleNr = Console.ReadLine();

            var content = File.ReadAllLines($"Resources\\Example{exampleNr}.txt");
            var inputData = content.Select(x => int.Parse(x)).ToList();

            return inputData;
        }
    }
}
