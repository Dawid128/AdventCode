
namespace Day01_CalorieCounting
{
    internal static class InputDataHandler
    {
        public static List<int> ReadDataFromFile()
        {
            Console.Write("Read data from example: ");
            var exampleNr = Console.ReadLine();

            var content = File.ReadAllText($"Resources\\Example{exampleNr}.txt").Split($"{Environment.NewLine}{Environment.NewLine}");
            var inputData = content.Select(x => x.Split(Environment.NewLine).Select(y => int.Parse(y)).Sum()).OrderByDescending(x => x).ToList();

            return inputData;
        }
    }
}
