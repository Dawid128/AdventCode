using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day14_Polymerization.Services
{
    internal class PolymerizationHandlerPro
    {
        private static Dictionary<string, PolymerInfo> _polymersInfo = new();
        public static void Run(string inputPolymer, Dictionary<string, char> pairPolymers, int days)
        {
            _polymersInfo = pairPolymers.ToDictionary(x => x.Key, x => new PolymerInfo(x.Key, $"{x.Key[0]}{x.Value}", $"{x.Value}{x.Key[1]}"));
            for (int i = 0; i < inputPolymer.Length - 1; i++)
            {
                var firstChar = inputPolymer[i];
                var secondChar = inputPolymer[i + 1];

                _polymersInfo[$"{firstChar}{secondChar}"].IncreaseStore(1);
            }

            for (int i = 1; i <= days; i++)
            {
                NextDay();
                _polymersInfo = _polymersInfo.OrderByDescending(x => x.Value.Count).ToDictionary(x => x.Key, x => x.Value);
                foreach (var item in _polymersInfo)
                    Console.WriteLine($"After step {i:00}: Max Char: {item.Key} -> {item.Value.Count}");
                Console.WriteLine();
            }

            var uniqueChars = pairPolymers.Select(x => x.Key).Aggregate((x, y) => $"{x}{y}").Distinct().ToDictionary(x => x, x => _polymersInfo.Select(y => y.Value.GetCountLast(x)).Aggregate((x, y) => x + y));
            uniqueChars[inputPolymer.First()] += 1;
            uniqueChars = uniqueChars.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            Console.WriteLine($"Search key: {uniqueChars.First().Value - uniqueChars.Last().Value}");
            Console.ReadKey();
        }

        private static void NextDay()
        {
            foreach (var polymerInfo in _polymersInfo)
            {
                _polymersInfo[polymerInfo.Value.PrefixPolymer].Increase((ulong)polymerInfo.Value.Count);
                _polymersInfo[polymerInfo.Value.SuffixPolymer].Increase((ulong)polymerInfo.Value.Count);
            }

            foreach (var polymerInfo in _polymersInfo)
                polymerInfo.Value.Store();
        }
    }

    [DebuggerDisplay("Count: {Count}, CountTemp: {_countTemp}")]
    internal class PolymerInfo
    {
        private ulong _countTemp = 0;

        public string PolymerId { get; }
        public string PrefixPolymer { get; }
        public string SuffixPolymer { get;}
        public ulong Count { get; set; }

        public PolymerInfo(string polymerId, string prefixPolymer, string sufixPolymer)
        {
            PolymerId = polymerId;
            PrefixPolymer = prefixPolymer;
            SuffixPolymer = sufixPolymer;
        }

        public void IncreaseStore(ulong count) => Count += count;
        public void Increase(ulong count) => _countTemp += count;
        public void Store()
        {
            Count = _countTemp;
            _countTemp = 0;
        }
        public ulong GetCountLast(char polymerChar)
        {
            if (PolymerId.Last() == polymerChar) 
                return Count;

            return 0;
        }
    }
}
