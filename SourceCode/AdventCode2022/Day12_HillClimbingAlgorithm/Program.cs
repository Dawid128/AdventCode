
var input = File.ReadAllText($"Resources\\Example1.txt");

var score = Part1(input);
Console.WriteLine($"Result Part1: {score}");

score = Part2(input);
Console.WriteLine($"Result Part1: {score}");

//What is the fewest steps required to move from your current position to the location that should get the best signal?
object Part1(string input)
{
    var flatMap = input.Split(Environment.NewLine)
                       .Select(x => x.Select(y => (int)y - 97))
                       .Select((a, y) => a.Select((b, x) => new Position(x, y, b)))
                       .SelectMany(x => x)
                       .ToDictionary(x => x.ToString(), y => y);

    var startPosition = flatMap.First(x => x.Value.Lvl == -14).Value;
    startPosition.Lvl = 0;

    var endPosition = flatMap.First(x => x.Value.Lvl == -28).Value;
    endPosition.Lvl = 26;

    return GetMinimumStepsBetweenPositions(startPosition, endPosition, flatMap);
}

//What is the fewest steps required to move starting from any square with elevation a to the location that should get the best signal?
object Part2(string input)
{
    var flatMap = input.Split(Environment.NewLine)
                        .Select(x => x.Select(y => (int)y - 97))
                        .Select((a, y) => a.Select((b, x) => new Position(x, y, b)))
                        .SelectMany(x => x)
                        .ToDictionary(x => x.ToString(), y => y);

    var endPosition = flatMap.First(x => x.Value.Lvl == -28).Value;
    endPosition.Lvl = 26;

    flatMap.First(x => x.Value.Lvl == -14).Value.Lvl = 0;

    var min = int.MaxValue;
    foreach (var startPosition in flatMap.Where(x => x.Value.Lvl == 0).Select(x => x.Value))
    {
        var minTemp = GetMinimumStepsBetweenPositions(startPosition, endPosition, flatMap.ToDictionary(x => x.Key, y => y.Value));
        if (minTemp == -1)
            continue;

        if (minTemp < min)
            min = minTemp;
    }

    return min;
}

//Find way to end position in as few steps as possible
//flatMap is representing height of each possible position 
//Rule1 -> You can go to position +1 lvl more 
//Rule2 -> You can go to position the same and lvl's less
//Asumption1 -> You can not go to the same position
int GetMinimumStepsBetweenPositions(Position startPosition, Position endPosition, Dictionary<string, Position> flatMap)
{
    //Get positions where can move
    IEnumerable<Position> GetNextPositions(Position position)
    {
        foreach (var neighborKey in position.GetPossibleNeighbor())
            if (flatMap.TryGetValue(neighborKey, out var neighborPosition) && neighborPosition.Lvl - position.Lvl <= 1)
            {
                flatMap.Remove(neighborKey);
                yield return neighborPosition;
            }
    }

    var currentPositions = new List<Position>(new[] { startPosition });
    var currentStep = 0;
    while (currentPositions.Count > 0)
    {
        currentStep++;
        var tempCurrentPositions = new List<Position>();
        foreach (var currentPosition in currentPositions)
            foreach (var nextPosition in GetNextPositions(currentPosition))
            {
                //Return currentStep if you are on the end position
                if (nextPosition.ToString() == endPosition.ToString())
                    return currentStep;

                tempCurrentPositions.Add(nextPosition);
            }

        currentPositions = tempCurrentPositions;
    }

    return -1;
}

public class Position
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Lvl { get; set; } //Height of position

    public Position(int x, int y, int lvl)
    {
        X = x;
        Y = y;
        Lvl = lvl;
    }

    public override string ToString() => $"X:{X},Y:{Y}";

    public IEnumerable<string> GetPossibleNeighbor()
    {
        yield return $"X:{X + 1},Y:{Y}";
        yield return $"X:{X - 1},Y:{Y}";
        yield return $"X:{X},Y:{Y + 1}";
        yield return $"X:{X},Y:{Y - 1}";
    }
}