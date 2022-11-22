
namespace Day06_Lanternfish.Models
{
    internal class Lanterfish
    {
        public short Time { get; private set; }

        public bool CanSpawn { get => Time is -1; }

        public Lanterfish() : this(8){}
        public Lanterfish(short time)
        {
            Time = time;
        }

        public Lanterfish SpawnNew()
        {
            if (Time is not -1)
                throw new Exception("Lanterfish can not spawn new");

            Time = 6;
            return new Lanterfish();
        }

        public void NextDay() => Time--;
    }
}
