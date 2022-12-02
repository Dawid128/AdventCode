using Day02_RockPaperScissors;

var movements = InputDataHandler.ReadDataFromFile();

//Part 1
Dictionary<string, int> allPossibleMovementsPart1 = new()
{
    { "AX", 4 },
    { "BX", 1 },
    { "CX", 7 },

    { "AY", 8 },
    { "BY", 5 },
    { "CY", 2 },

    { "AZ", 3 },
    { "BZ", 9 },
    { "CZ", 6 },
};


var score = 0;
foreach (var move in movements)
{
    score += allPossibleMovementsPart1[move];
}

Console.WriteLine($"Result Part1: {score}");

//Part 2
Dictionary<string, int> allPossibleMovementsPart2 = new()
{
    { "AX", 0 + 3 },
    { "BX", 0 + 1 },
    { "CX", 0 + 2 },

    { "AY", 3 + 1 },
    { "BY", 3 + 2 },
    { "CY", 3 + 3 },

    { "AZ", 6 + 2 },
    { "BZ", 6 + 3 },
    { "CZ", 6 + 1 },
};


score = 0;
foreach (var move in movements)
{
    score += allPossibleMovementsPart2[move];
}

Console.WriteLine($"Result Part2: {score}");