
namespace Day14_Polymerization.Services
{
    internal static class PolymerizationHandler
    {
        public static void Run(string inputPolymer, Dictionary<string, char> pairPolymers, int days)
        {
            Console.WriteLine($"Initial value: {inputPolymer}");
            for (int i = 1; i <= days; i++)
            {
                inputPolymer = NextDay(inputPolymer, pairPolymers);
                //Console.WriteLine($"After step {i:00}: {inputPolymer}");
                //Console.WriteLine($"After step {i:00}: {inputPolymer.Count()}");
            }
            Console.WriteLine($"Final Length: {inputPolymer.Length}");

            var charsGroups = inputPolymer.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count()).OrderByDescending(x => x.Value);
            Console.WriteLine($"Final Max Char: {charsGroups.First().Key} -> {charsGroups.First().Value}");
            Console.WriteLine($"Final Min Char: {charsGroups.Last().Key} -> {charsGroups.Last().Value}");
            Console.WriteLine($"Search key: {charsGroups.First().Value - charsGroups.Last().Value}");
            Console.ReadKey();
        }

        private static string NextDay(string inputPolymer, Dictionary<string, char> pairPolymers)
        {
            var result = $"{inputPolymer.First()}";
            for (int i = 0; i < inputPolymer.Length - 1; i++) 
            {
                var firstChar = inputPolymer[i];
                var secondChar = inputPolymer[i + 1];
                var searchChar = pairPolymers[$"{firstChar}{secondChar}"];

                result += $"{searchChar}{secondChar}";
            }

            return result;
        }
    }
}
