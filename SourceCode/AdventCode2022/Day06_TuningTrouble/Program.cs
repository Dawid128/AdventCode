
Console.Write("Read data from example: ");
var exampleNr = Console.ReadLine();

var input = File.ReadAllText($"Resources\\Example{exampleNr}.txt");

var score = Part1(input);
Console.WriteLine($"Result Part1: {score}");

score = Part2(input);
Console.WriteLine($"Result Part2: {score}");

//Part 1
//How many characters need to be processed before the first start-of-packet marker is detected? -> required 4 distinct characters in packet 
object Part1(string input)
{
    for (int i = 3; i < input.Length; i++)
    {
        var temp = input.Substring(i - 3, 4);
        if (temp.Distinct().Count() == 4)
            return i + 1;
    }

    return -1;
}

//Part 2
//How many characters need to be processed before the first start-of-message marker is detected? -> required 14 distinct characters in packet 
object Part2(string input)
{
    for (int i = 13; i < input.Length; i++)
    {
        var temp = input.Substring(i - 13, 14);
        if (temp.Distinct().Count() == 14)
            return i + 1;
    }

    return -1;
}