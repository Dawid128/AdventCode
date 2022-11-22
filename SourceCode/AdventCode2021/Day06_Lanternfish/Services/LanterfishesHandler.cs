using Day06_Lanternfish.Models;

namespace Day06_Lanternfish.Services
{
    internal class LanterfishesHandler
    {
        private short currentDay = 0;

        public void Run(List<Lanterfish> lanterfishes, short days)
        {
            currentDay = 0;
            ConsoleHandler.WriteLanterfishesCountInitial(lanterfishes);
            while (days > 0)
            {
                NextDay(lanterfishes);
                days--;
            }
            ConsoleHandler.WriteLanterfishesFinalCount(lanterfishes);
        }

        private void NextDay(List<Lanterfish> lanterfishes)
        {
            var newLanterfishes = new List<Lanterfish>();
            foreach (var lanterfish in lanterfishes)
            {
                lanterfish.NextDay();
                if (lanterfish.CanSpawn is false)
                    continue;

                newLanterfishes.Add(lanterfish.SpawnNew());
            }

            lanterfishes.AddRange(newLanterfishes);
            currentDay++;

            ConsoleHandler.WriteLanterfishesCountAfterDay(lanterfishes, currentDay);
        }
    }
}
