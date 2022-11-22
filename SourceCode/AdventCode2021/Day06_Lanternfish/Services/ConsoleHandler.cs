using Day06_Lanternfish.Models;

namespace Day06_Lanternfish.Services
{
    internal static class ConsoleHandler
    {
        public static void WriteLanterfishesInitial(IList<Lanterfish> lanterfishes)
        {
            var dataStr = lanterfishes.Select(x => x.Time.ToString()).Aggregate((x, y) => $"{x},{y}");
            Console.WriteLine($"Initial state: {dataStr}");
        }
        public static void WriteLanterfishesCountInitial(IList<Lanterfish> lanterfishes)
        {
            Console.WriteLine($"Initial count: {lanterfishes.Count}");
        }

        public static void WriteLanterfishesAfterDay(IList<Lanterfish> lanterfishes, int day)
        {
            var dataStr = lanterfishes.Select(x => x.Time.ToString()).Aggregate((x, y) => $"{x},{y}");
            Console.WriteLine($"After {day:00} days: {dataStr}");
        }

        public static void WriteLanterfishesCountAfterDay(IList<Lanterfish> lanterfishes, int day)
        {
            Console.WriteLine($"After {day:00} days count: {lanterfishes.Count}");
        }

        public static void WriteLanterfishesFinalCount(IList<Lanterfish> lanterfishes)
        {
            Console.WriteLine($"Final count: {lanterfishes.Count}");
        }

        public static List<Lanterfish> ReadLanterfishesFromCommandLineArgs()
        {
            var args = Environment.GetCommandLineArgs();
            if (args.Length < 2)
                return new List<Lanterfish>();

            return ReadLanterfishesFromInputData(args[1]);
        }

        public static short ReadRequestDaysCommandLineArgs()
        {
            var args = Environment.GetCommandLineArgs();
            if (args.Length < 3)
                return 0;

            return ReadRequestDaysFromInputData(args[2]);
        }

        public static List<Lanterfish> ReadLanterfishesFromCommandLine()
        {
            Console.Write("Input Data: ");
            var inputData = Console.ReadLine();
            if (inputData is null)
                return new List<Lanterfish>();

            return ReadLanterfishesFromInputData(inputData);
        }

        private static List<Lanterfish> ReadLanterfishesFromInputData(string inputData)
        {
            var result = new List<Lanterfish>();
            foreach (var time in inputData.Split(','))
            {
                if (!short.TryParse(time, out var timeShort) || timeShort < 0 || timeShort > 5)
                    throw new Exception("Invalid input data [Lanterfishes]");

                result.Add(new Lanterfish(timeShort));
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
