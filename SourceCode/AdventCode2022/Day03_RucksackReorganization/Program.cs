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
                     .Select(SplitToHalf)
                     .SelectMany(x => x.Part1.Intersect(x.Part2))
                     .Select(ConvertCharToScore)
                     .Sum();

    return score;
}

//Part 2
int Part2(string input)
{
    var score = input.Split(Environment.NewLine)
                     .Chunk(3)
                     .Select(x => x.Select(x => x.Distinct().ToHashSet()))
                     .SelectMany(IntersectMulti)
                     .Select(ConvertCharToScore)
                     .Sum();

    return score;
}

static (string Part1, string Part2) SplitToHalf(string input)
{
    var half = input.Length / 2;
    var firstPart = input.Substring(0, half);
    var secondPart = input.Substring(half, half);

    return (firstPart, secondPart);
}

static HashSet<char> IntersectMulti(IEnumerable<HashSet<char>> input)
{
    var result = input.First();
    foreach (var nextList in input.Skip(1))
        result = result.Intersect(nextList).ToHashSet();

    return result;
}

static int ConvertCharToScore(char character)
{
    if (character >= 'a' && character <= 'z')
        return character - 96;

    return character - 38;
}