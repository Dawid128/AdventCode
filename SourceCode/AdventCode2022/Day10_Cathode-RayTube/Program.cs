
int totalCycle;
int totalRegister;

var input = File.ReadAllText($"Resources\\Example1.txt");

var score = Part1(input, new[] { 20, 60, 100, 140, 180, 220 });
Console.WriteLine($"Result Part1: {score}");

Console.WriteLine($"Result Part2:");
Part2(input);

//Get sum of ratio of cycle and register number for defined cycles
object Part1(string input, IList<int> sumCycles)
{
    var cycles = GetCyclesCPU(input);
    var score = sumCycles.Sum(x => cycles[x] * x);
    return score;
}

//Get cacles CPU and draw in CRT
object Part2(string input)
{
    var cycles = GetCyclesCPU(input);
    Draw(cycles);
    return 0;
}

//Get register number each cycles CPU for input data
Dictionary<int, int> GetCyclesCPU(string input)
{
    totalCycle = 0;
    totalRegister = 1;

    var cycles = input.Split(Environment.NewLine)
                     .SelectMany(GetNextCycleCPU)
                     .ToDictionary(x => x.TotalCycle, x => x.TotalRegister);
    return cycles;
}

//Get register number next cycles CPU for input instruction
IEnumerable<(int TotalCycle, int TotalRegister)> GetNextCycleCPU(string input)
{
    totalCycle++;

    if (input.StartsWith("noop"))
        yield return (totalCycle, totalRegister);
    else
    {
        var register = int.Parse(input[4..]);

        yield return (totalCycle, totalRegister);
        totalCycle += 1;
        yield return (totalCycle, totalRegister);
        totalRegister += register;
    }
}

//Draw code eight capital letters appear base on procesor result 
//CRT monitor has resolution 40x6
void Draw(Dictionary<int, int> cyclesCPU)
{
    var cycle = 1;
    var row = 0;
    var column = 0;

    while (row++ < 6)
    {
        while (column++ < 40)
        {
            var spritePosition = cyclesCPU[cycle] + 1; //Middle position of sprite

            if (Math.Abs(column - spritePosition) <= 1)
                Console.Write("#");
            else
                Console.Write(".");

            cycle++;
        }
        column = 0;
        Console.WriteLine();
    }
}