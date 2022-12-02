
namespace Day02_RockPaperScissors
{
    internal static class InputDataHandler
    {
        public static List<string> ReadDataFromFile()
        {
            Console.Write("Read data from example: ");
            var exampleNr = Console.ReadLine();

            var content = File.ReadAllText($"Resources\\Example{exampleNr}.txt").Split($"{Environment.NewLine}");
            var inputData = content.Select(x => x.Replace(" ", "")).ToList();


            return inputData;
        }
    }
}
