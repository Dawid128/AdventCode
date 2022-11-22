using System.Diagnostics;

namespace Day06_Lanternfish_Perfo1.Models
{
    [DebuggerDisplay("Count: {Count}, CountNew: {CountNew}")]
    internal class LanterfishesGroupInfo
    {
        public ulong Count { get; set; }
        public ulong CountNew { get; set; }

        public LanterfishesGroupInfo(ulong count)
        {
            Count = count;
        }
    }
}
