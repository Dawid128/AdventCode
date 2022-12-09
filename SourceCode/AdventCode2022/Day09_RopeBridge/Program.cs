
var input = File.ReadAllText($"Resources\\Example1.txt");

var score = Part1(input);
Console.WriteLine($"Result Part1: {score}");

score = Part2(input);
Console.WriteLine($"Result Part2: {score}");

//Part 1 
//Count visited positions by tail of snake with length 2
object Part1(string input) => GetScore(input, 2);

//Part 2
//Count visited positions by tail of snake with length 10
object Part2(string input) => GetScore(input, 10);

//Count visited positions by tail of snake with defined length
object GetScore(string input, int snakeLength)
{
    var instructions = input.Split(Environment.NewLine)
                        .Select(x => new { Direction = Enum.Parse<Direction>(x[0].ToString()), Steps = int.Parse(x[2..]) })
                        .ToList();

    var vtPositions = new HashSet<string>(); //Visited tail positions
    var cbPositions = Enumerable.Range(0, snakeLength).Select(x => new Position()).ToList(); //Current body positions with first head and last tail

    foreach (var instruction in instructions)
        for (int i = 0; i < instruction.Steps; i++)
        {
            var chPosition = cbPositions[0]; //current head position
            ChangeCurrentHeadPosition(ref chPosition, instruction.Direction);
            for (int j = 1; j < cbPositions.Count; j++)
            {
                var cpPosition = cbPositions[j - 1]; //previos position in body
                var cnPosition = cbPositions[j]; //next position in body

                if (cnPosition.IsContact(cpPosition))
                    break;

                SetNextBodyPosition(ref cnPosition, cpPosition);
            }

            var ctPositionStr = cbPositions[^1].ToString(); //Get position of tail as string
            if (vtPositions.Contains(ctPositionStr))
                continue;

            vtPositions.Add(ctPositionStr);
        }

    var score = vtPositions.Count;
    return score;
}

//Change cordinates of head
void ChangeCurrentHeadPosition(ref Position position, Direction direction) =>
    _ = direction switch
    {
        Direction.U => position.Y++,
        Direction.L => position.X--,
        Direction.D => position.Y--,
        Direction.R => position.X++,
        _ => throw new NotImplementedException()
    };

//Set next position in the body, base on previous position
void SetNextBodyPosition(ref Position nPosition, Position pPosition)
{
    if (!pPosition.IsSameHorizontalLine(nPosition))
        _ = pPosition.X > nPosition.X ? nPosition.X++ : nPosition.X--;

    if (!pPosition.IsSameVerticalLine(nPosition))
        _ = pPosition.Y > nPosition.Y ? nPosition.Y++ : nPosition.Y--;
}

public enum Direction
{
    U = 1,
    L = 2,
    D = 3,
    R = 4
}

public class Position
{
    public int X { get; set; }
    public int Y { get; set; }

    //Return true, if is touches (acceptable also cross)
    public bool IsContact(Position position)
    {
        if (Math.Abs(X - position.X) > 1 || Math.Abs(Y - position.Y) > 1)
            return false;

        return true;
    }

    //Return true, if the same horizontal or vertical line
    public bool IsSameHorizontalLine(Position position) => position.X == X;
    public bool IsSameVerticalLine(Position position) => position.Y == Y;

    public override string ToString() => $"X:{X},Y:{Y}";
}