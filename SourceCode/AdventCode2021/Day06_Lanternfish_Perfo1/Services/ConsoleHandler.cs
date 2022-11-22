using Day06_Lanternfish_Perfo1.Models;

namespace Day06_Lanternfish_Perfo1.Services
{
    internal static class ConsoleHandler
    {
        public static void WriteLanterfishesCountInitial(ulong lanterfishesCount)
        {
            Console.WriteLine($"Initial count: {lanterfishesCount}");
        }

        public static void WriteLanterfishesCountAfterDay(ulong lanterfishesCount, int day)
        {
            Console.WriteLine($"After {day:00} days count: {lanterfishesCount}");
        }

        public static void WriteLanterfishesFinalCount(ulong lanterfishesCount)
        {
            Console.WriteLine($"Final count: {lanterfishesCount}");
        }

        public static List<int> ReadLanterfishesFromCommandLineArgs()
        {
            var args = Environment.GetCommandLineArgs();
            if (args.Length < 2)
                return new List<int>();

            return ReadLanterfishesFromInputData(args[1]);
        }

        public static short ReadRequestDaysCommandLineArgs()
        {
            var args = Environment.GetCommandLineArgs();
            if (args.Length < 3)
                return 0;

            return ReadRequestDaysFromInputData(args[2]);
        }

        public static List<int> ReadLanterfishesFromCommandLine()
        {
            Console.Write("Input Data: ");
            var inputData = Console.ReadLine();
            if (inputData is null)
                return new List<int>();

            return ReadLanterfishesFromInputData(inputData);
        }

        public static short ReadRequestDaysCommandLine()
        {
            Console.Write("Input Request Days: ");
            var inputData = Console.ReadLine();
            if (inputData is null)
                return 0;

            return ReadRequestDaysFromInputData(inputData);
        }

        private static List<int> ReadLanterfishesFromInputData(string inputData)
        {
            var result = new List<int>();
            foreach (var time in inputData.Split(','))
            {
                if (!int.TryParse(time, out var timeInt) || timeInt < 0 || timeInt > 8)
                    throw new Exception("Invalid input data [Lanterfishes]");

                result.Add(timeInt);
            }

            return result;
        }

        private static short ReadRequestDaysFromInputData(string inputData)
        {
            if (!short.TryParse(inputData, out var timeShort))
                throw new Exception("Invalid input data [RequestDays]");

            return timeShort;
        }
    }
}
