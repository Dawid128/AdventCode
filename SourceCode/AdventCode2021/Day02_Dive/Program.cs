using Day02_Dive.Models;
using Day02_Dive.Services;

while (true)
{
    var moveInstructions = InputDataHandler.ReadDataFromFile();

    //Part 1
    var forwardTotal = 0;
    var depthTotal = 0;
    foreach (var moveInstruction in moveInstructions)
    {
        if (moveInstruction.Type == MoveInstructionType.Forward)
            forwardTotal += moveInstruction.Value;
        else
            depthTotal += moveInstruction.Value;
    }

    var result = forwardTotal * depthTotal;
    Console.WriteLine($"Result Part1: {result}");

    //Part 2
    forwardTotal = 0;
    depthTotal = 0;
    var aim = 0;
    foreach (var moveInstruction in moveInstructions)
    {
        if (moveInstruction.Type == MoveInstructionType.Forward)
        {
            forwardTotal += moveInstruction.Value;
            depthTotal += aim * moveInstruction.Value;
            continue;
        }

        aim += moveInstruction.Value;
    }

    result = forwardTotal * depthTotal;
    Console.WriteLine($"Result Part2: {result}");

    Console.ReadKey();

}