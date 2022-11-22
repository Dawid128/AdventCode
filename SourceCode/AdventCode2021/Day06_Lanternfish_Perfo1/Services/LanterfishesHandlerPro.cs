using Day06_Lanternfish_Perfo1.Models;

namespace Day06_Lanternfish_Perfo1.Services
{
    internal class LanterfishesHandlerPro
    {
        //Count lanterfishes in different groups age. 
        private readonly Dictionary<int, LanterfishesGroupInfo> lanterfishesCountInGroups = new();

        private short currentDay = 0;

        public void Run(List<int> lanterfishes, short days)
        {
            currentDay = 0;
            lanterfishesCountInGroups.Clear();
            for (int i = 0; i <= 8; i++)
                lanterfishesCountInGroups.Add(i, new LanterfishesGroupInfo((ulong)lanterfishes.Count(x => x == i)));

            ConsoleHandler.WriteLanterfishesCountInitial(lanterfishesCountInGroups.Select(x => x.Value.Count).Aggregate((x, y) => x + y));
            while (days > 0)
            {
                NextDay();
                days--;
            }
            ConsoleHandler.WriteLanterfishesFinalCount(lanterfishesCountInGroups.Select(x => x.Value.Count).Aggregate((x, y) => x + y));
        }

        private void NextDay()
        {
            currentDay++;

            for (int i = 8; i >= 0; i--)
            {
                var lanterfishesCountInGroup = lanterfishesCountInGroups[i].Count;
                lanterfishesCountInGroups[i].Count = 0;

                if (i > 0)
                {
                    lanterfishesCountInGroups[i - 1].CountNew = lanterfishesCountInGroup;
                    lanterfishesCountInGroups[i].Count = 0;
                    continue;
                }

                lanterfishesCountInGroups[6].CountNew += lanterfishesCountInGroup;
                lanterfishesCountInGroups[8].CountNew += lanterfishesCountInGroup;
                lanterfishesCountInGroups[0].Count -= 0;
            }

            for (int i = 0; i <= 8; i++)
            {
                lanterfishesCountInGroups[i].Count = lanterfishesCountInGroups[i].CountNew;
                lanterfishesCountInGroups[i].CountNew = 0;
            }

            var lanterfishesCount = lanterfishesCountInGroups.Select(x => x.Value.Count).Aggregate((x, y) => x + y);
            ConsoleHandler.WriteLanterfishesCountAfterDay(lanterfishesCount, currentDay);
        }
    }
}
