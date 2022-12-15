
var input = File.ReadAllText($"Resources\\Example1.txt");

var score = Part1(input, true);
Console.WriteLine($"Result Part1: {score}");

score = Part2(input, false);
Console.WriteLine($"Result Part2: {score}");

//Part 1 
//How many units of sand come to rest before sand starts flowing into the abyss below?
object Part1(string input, bool endlessVoidUnder) => GetScore(input, new(500, 0), true);

//Part 2
//How many units of sand come to rest?
object Part2(string input, bool endlessVoidUnder) => GetScore(input, new(500, 0), false);

//startPosition -> point of pouring sand into the cave
object GetScore(string input, Point startPosition, bool endlessVoidUnder)
{
    var instructions = GetInstructionsDrawOfStones(input);

    var map = GetMap(instructions, startPosition, endlessVoidUnder);
    var score = CountSteps(map, startPosition);
    return score;
}

List<List<Point>> GetInstructionsDrawOfStones(string input)
{
    Point SelectPosition(string input)
    {
        var temp = input.Split(",");
        return new(int.Parse(temp[0]), int.Parse(temp[1]));
    }

    var instructions = input.Split(Environment.NewLine)
                        .Select(x => x.Split(" -> ")
                                      .Select(SelectPosition)
                                      .ToList())
                        .ToList();

    return instructions;
}

int CountSteps(bool[][] map, Point startPosition)
{
    var count = 0;
    while (true)
    {
        var (finish, newX, newY) = GetNextPositionSand(map, startPosition);
        if (finish is true)
            return newX + newY == 0 ? ++count : count; //Increase count 1 only if sand touches point of pouring 

        map[newY][newX] = true;
        count++;
    }
}

(bool Finish, int X, int Y) GetNextPositionSand(bool[][] map, Point startPosition)
{
    int currentX = startPosition.X;
    int currentY = startPosition.Y;

    bool GoDown() => !map[currentY + 1][currentX];
    bool GoLeft() => !map[currentY + 1][currentX - 1];
    bool GoRight() => !map[currentY + 1][currentX + 1];

    while (true)
        try
        {
            if (GoDown())
            {
                currentY++;
                continue;
            }

            if (GoLeft())
            {
                currentY++;
                currentX--;
                continue;
            }

            if (GoRight())
            {
                currentY++;
                currentX++;
                continue;
            }

            if (currentY == 0)
            {
                //Sand touches point of pouring [Part 2]
                return (true, 0, 0);
            }

            break;
        }
        catch
        {
            //Sand fall into the endless void [Part 1]
            return (true, -1, -1);
        }

    return (false, currentX, currentY);
}

//Generate map from instructions where are stones 
//Use endlessVoidUnder if you don't want create infinite floor
bool[][] GetMap(List<List<Point>> instructions, Point startPosition, bool endlessVoidUnder)
{
    var allPositions = instructions.SelectMany(x => x).ToList();
    allPositions.Add(startPosition);

    var xMax = allPositions.Max(m => m.X);
    var xMin = allPositions.Min(m => m.X);
    var yMax = allPositions.Max(m => m.Y);
    var yMin = allPositions.Min(m => m.Y);
    var w = xMax - xMin + 1;
    var h = yMax - yMin + 1;

    //Corigate the size map
    //Only if required
    if (!endlessVoidUnder)
    {
        h += 2;
        xMin -= h;
        xMax += h;
        w = xMax - xMin + 1;
    }

    //Corigate the initial positions [Minimalize map]
    for (int i = 0; i < allPositions.Count; i++)
    {
        var position = allPositions[i];
        position.X -= xMin;
        position.Y -= yMin;
    }

    //Initialize the map with size height x width
    var map = new bool[h][];
    foreach (var rowId in Enumerable.Range(0, h))
        map[rowId] = new bool[w];

    //Complete the map -> check where are stones
    foreach (var instruction in instructions)
        for (int i = 1; i < instruction.Count; i++)
            foreach (var point in GetAllPositionsBetween(instruction[i - 1], instruction[i]))
                map[point.Y][point.X] = true;

    //Create infinite floor -> 2 lvls under last stone.  
    //Only if required
    if (!endlessVoidUnder)
        for (int x = 0; x < w; x++)
            map[h - 1][x] = true;

    return map;
}

//Get all available positions between 2 positions (with these) 
//Position1=5,0, Position2=7,0 -> 5,0 ; 6,0 ; 7;0
static IEnumerable<Point> GetAllPositionsBetween(Point position1, Point position2)
{
    var minX = Math.Min(position1.X, position2.X);
    var minY = Math.Min(position1.Y, position2.Y);
    var maxX = Math.Max(position1.X, position2.X);
    var maxY = Math.Max(position1.Y, position2.Y);

    for (int x = minX; x <= maxX; x++)
        for (int y = minY; y <= maxY; y++)
            yield return new(x, y);
}

public class Point
{
    public int X { get; set; }
    public int Y { get; set; }
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}