using Day04_GiantSquid.Models;

namespace Day04_GiantSquid
{
    internal static class InputDataHandler
    {
        public static (List<int> BingoNumbers, List<BingoGrid> BingoGrids) ReadDataFromFile()
        {
            Console.Write("Read data from example: ");
            var exampleNr = Console.ReadLine();

            var content = File.ReadAllText($"Resources\\Example{exampleNr}.txt").Split($"{Environment.NewLine}{Environment.NewLine}");

            var bingoNumbers = content[0].Split(",").Select(x => int.Parse(x)).ToList();
            var bingoGrids = content.Skip(1).Select(x => new BingoGrid(x)).ToList();

            return (bingoNumbers, bingoGrids);
        }
    }
}
