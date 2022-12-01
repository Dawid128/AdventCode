using Day02_Dive.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02_Dive.Services
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

        public static List<MoveInstruction> ReadDataFromFile()
        {
            Console.Write("Read data from example: ");
            var exampleNr = Console.ReadLine();

            var content = File.ReadAllLines($"Resources\\Example{exampleNr}.txt");
            var inputData = content.Select(x=>x.Split(" ")).Select(x => new MoveInstruction(x[0], int.Parse(x[1]))).ToList();

            return inputData;
        }
    }
}
