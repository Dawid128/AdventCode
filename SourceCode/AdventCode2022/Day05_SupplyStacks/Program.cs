using System.Text.RegularExpressions;

Console.Write("Read data from example: ");
var exampleNr = Console.ReadLine();

var input = File.ReadAllText($"Resources\\Example{exampleNr}.txt");

var score = Part1(input);
Console.WriteLine($"Result Part1: {score}");

score = Part2(input);
Console.WriteLine($"Result Part2: {score}");

//Part 1
object Part1(string input)
{
    var data = input.Split($"{Environment.NewLine}{Environment.NewLine}");

    var layout = ParseToLayoutStack(data[0]);
    var instructions = ParseToInstructions(data[1]);

    foreach (var (count, from, to) in instructions)
        for (int i = 0; i < count; i++)
            layout[to].Push(layout[from].Pop());

    //Create string from top crate each stacks
    var result = new string(layout.Select(x => x.Value.Peek()).ToArray());
    return result;
}

//Part 2
object Part2(string input)
{
    var score = input.Split(Environment.NewLine);

    var data = input.Split($"{Environment.NewLine}{Environment.NewLine}");

    var layout = ParseToLayoutList(data[0]);
    var instructions = ParseToInstructions(data[1]);

    foreach (var (count, from, to) in instructions)
    {
        layout[to].InsertRange(0, layout[from].GetRange(0, count));
        layout[from].RemoveRange(0, count);
    }

    //Create string from top crate each stacks
    var result = new string(layout.Select(x => x.Value.First()).ToArray());
    return result;
}

///Get layout of crates in stacks
///The collection of crates is represented by stack -> Cargo crane need to move crates one by one
Dictionary<int, Stack<char>> ParseToLayoutStack(string input)
{
    var lines = input.Split(Environment.NewLine);

    var regex = new Regex(@"\d+");
    var result = regex.Matches(lines.Last()).Select(x => int.Parse(x.Value)).ToDictionary(x => x, y => new Stack<char>());

    for (int rowId = lines.Length - 2; rowId >= 0; rowId--) 
        foreach (var item in result)
        {
            var character = lines[rowId][((item.Key - 1) * 4) + 1];
            if (character == 32)
                continue;

            item.Value.Push(character);
        }

    return result;
}

///Get layout of crates in stacks
///The collection of crates is represented by list -> Cargo crane can to move all crates from stack in one move
Dictionary<int, List<char>> ParseToLayoutList(string input)
{
    var lines = input.Split(Environment.NewLine);

    var regex = new Regex(@"\d+");
    var result = regex.Matches(lines.Last()).Select(x => int.Parse(x.Value)).ToDictionary(x => x, y => new List<char>());

    for (int rowId = 0; rowId <= lines.Length - 2; rowId++)
        foreach (var item in result)
        {
            var character = lines[rowId][((item.Key - 1) * 4) + 1];
            if (character == 32)
                continue;

            item.Value.Add(character);
        }

    return result;
}

///Get List of instructions of cargo crane
List<(int Count, int From, int To)> ParseToInstructions(string input)
{
    var regex = new Regex(@"\d+");
    var result = input.Split(Environment.NewLine)
                      .Select(x => regex.Matches(x)
                                        .Select(y => int.Parse(y.Value))
                                        .ToArray())
                      .Select(x => (x[0], x[1], x[2]))
                      .ToList();

    return result;
}