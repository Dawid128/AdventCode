
var input = File.ReadAllText($"Resources\\Example1.txt");

var score = Part1(input);
Console.WriteLine($"Result Part1: {score}");

score = Part2(input);
Console.WriteLine($"Result Part2: {score}");

//Part 1
//Get score visible count of trees 
object Part1(string input)
{
    var map = input.Split(Environment.NewLine)
                   .Select(x => x.Select(y => int.Parse(y.ToString()))
                                 .ToArray())
                   .ToArray();

    //Count tree if visible from outside
    HashSet<string> positions = new();
    void CountIfTrue(int x, int y, ref int max)
    {
        var value = map[y][x];
        if (max >= value)
            return;

        var key = $"X:{x},Y:{y}";
        if (!positions.Contains(key))
            positions.Add(key);
        max = value;
    }

    var countColumns = map.Length;
    var countRows = map[0].Length;

    //Count visible trees in columns
    for (int y = 1; y < countColumns - 1; y++)
    {
        var max = map[y][0];
        for (int x = 1; x < countRows - 1; x++)
            CountIfTrue(x, y, ref max);

        max = map[y][countRows - 1];
        for (int x = countRows - 2; x > 0; x--)
            CountIfTrue(x, y, ref max);
    }

    //Count visible trees in rows
    for (int x = 1; x < countRows - 1; x++)
    {
        var max = map[0][x];
        for (int y = 1; y < countColumns - 1; y++)
            CountIfTrue(x, y, ref max);

        max = map[countColumns - 1][x];
        for (int y = countColumns - 2; y > 0; y--)
            CountIfTrue(x, y, ref max);
    }

    var score = (countRows * 2) + (countColumns * 2) - 4 + positions.Count;
    return score;
}

//Part 2
//Get score the tree with the most trees view
object Part2(string input)
{
    var map = input.Split(Environment.NewLine)
               .Select(x => x.Select(y => int.Parse(y.ToString()))
                             .ToArray())
               .ToArray();

    var countColumns = map.Length;
    var countRows = map[0].Length;

    //Get count visible trees left side
    int FindCountTreeLeft(int x, int y, int value)
    {
        int count = 0;
        while (x-- > 0)
        {
            count++;
            if (map[y][x] >= value)
                break;
        }

        return count;
    }

    //Get count visible trees right side
    int FindCountTreeRight(int x, int y, int value)
    {
        int count = 0;
        while (x++ < countRows - 1) 
        {
            count++;
            if (map[y][x] >= value)
                break;
        }

        return count;
    }

    //Get count visible trees top side
    int FindCountTreeTop(int x, int y, int value)
    {
        int count = 0;
        while (y-- > 0)
        {
            count++;
            if (map[y][x] >= value)
                break;
        }

        return count;
    }

    //Get count visible trees bottom side
    int FindCountTreeBottom(int x, int y, int value)
    {
        int count = 0;
        while (y++ < countColumns - 1) 
        {
            count++;
            if (map[y][x] >= value)
                break;
        }

        return count;
    }

    var max = 0;
    for (int x = 0; x < countRows; x++)
        for (int y = 0; y < countColumns; y++)
        {
            var value = map[y][x];
            var countLeft = FindCountTreeLeft(x, y, value);
            var countRight = FindCountTreeRight(x, y, value);
            var countTop = FindCountTreeTop(x, y, value);
            var countBottom = FindCountTreeBottom(x, y, value);

            max = Math.Max(max, countLeft * countRight * countTop * countBottom);
        }

    return max;
}