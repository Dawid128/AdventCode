
namespace Day02_Dive.Models
{
    internal class MoveInstruction
    {
        public MoveInstructionType Type { get; set; }
        public int Value { get; set; }

        public MoveInstruction(string typeStr, int value)
        {
            switch (typeStr)
            {
                case "forward":
                    Type = MoveInstructionType.Forward;
                    Value = value;
                    break;

                case "down":
                    Type = MoveInstructionType.Depth;
                    Value = value;
                    break;

                case "up":
                    Type = MoveInstructionType.Depth;
                    Value = -value;
                    break;
            }
        }
    }

    internal enum MoveInstructionType
    {
        Forward,
        Depth,
    }
}
