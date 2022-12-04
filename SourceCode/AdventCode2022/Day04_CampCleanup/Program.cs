Console.Write("Read data from example: ");
var exampleNr = Console.ReadLine();

var input = File.ReadAllText($"Resources\\Example{exampleNr}.txt");

var score = Part1(input);
Console.WriteLine($"Result Part1: {score}");

score = Part2(input);
Console.WriteLine($"Result Part2: {score}");

//Part 1
int Part1(string input)
{
    var score = input.Split(Environment.NewLine)
                     .Select(S1)
                     .Count(x => x[0].Contains(x[1]) || x[1].Contains(x[0]));

    return score;
}

//Part 2
int Part2(string input)
{
    var score = input.Split(Environment.NewLine)
                     .Select(S1)
                     .Count(x => x[0].Overlap(x[1]));

    return score;
}

//Select ranges of 2 elves in pair
Range[] S1(string input)
{
    var parts = input.Split(',');
    return new[] { S2(parts[0]), S2(parts[1]) };
}

//Select range of 1 elf
Range S2(string input)
{
    var parts = input.Split('-');
    return new Range(int.Parse(parts[0]), int.Parse(parts[1]));
}

struct Range
{
    public int Start;
    public int End;

    public Range(int start, int end)
    {
        Start = start;
        End = end;
    }

    //If this range contains requested range
    public bool Contains(Range range)
    {
        if (range.Start >= Start && range.End <= End)
            return true;

        return false;
    }

    //If this range overlap requested range
    public bool Overlap(Range range)
    {
        if (range.Start > End || range.End < Start) 
            return false;

        return true;
    }
}