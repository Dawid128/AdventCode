
var input = File.ReadAllText($"Resources\\Example1.txt");

var score = Part1(input, 20);
Console.WriteLine($"Result Part1: {score}");

score = Part2(input, 10000);
Console.WriteLine($"Result Part1: {score}");

//Get product of 2 max numbers. The numbe is representing how many time monkey was throw item
//Get score after 20 rounds
//In Part1 before each trow, worried level is reduced 3 times
object Part1(string input, int countRounds) => GetScore(input, countRounds, 3);

//Get product of 2 max numbers. The numbe is representing how many time monkey was throw item
//Get score after 10 000 rounds
//In Part2 before each trow, worried level is not chaning like in part1. 
//Current solution Need 470s to get score 
object Part2(string input, int countRounds) => GetScore(input, countRounds, 1);

ulong GetScore(string input, int countRounds, int divideBy)
{
    var monkeys = input.Split($"{Environment.NewLine}{Environment.NewLine}")
                       .Select(SelectMonkey)
                       .ToList();

    var commonDivisor = monkeys.Select(x => x.TestValue).Aggregate((a, b) => a * b); //Common divisor each condition of monkey
    while (countRounds-- > 0)
        NextRound(monkeys, divideBy, commonDivisor);

    //How many time monkey was throw item -> get sorted by descending
    var countThrows = monkeys.Select(x => x.GetCountThrows())
                             .OrderByDescending(x => x)
                             .ToList();

    var score = (ulong)countThrows[0] * (ulong)countThrows[1];
    return score;
}

//divideBy -> how many times reducts worried level before each throw
void NextRound(List<Monkey> monkeys, int divideBy, int commonDivisor)
{
    foreach (var monkey in monkeys)
        while (monkey.HasItemToThrow())
        {
            var (item, targetMonkeyIndex) = monkey.GetNextItemToThrow(divideBy, commonDivisor);
            var targetMonkey = monkeys[targetMonkeyIndex];
            targetMonkey.Items.Enqueue(item);
        }
}

Monkey SelectMonkey(string input)
{
    var data = input.Split(Environment.NewLine);

    var items = data[1].Replace("Starting items:", string.Empty)
                       .Replace(" ", string.Empty)
                       .Split(",")
                       .Select(x => ulong.Parse(x));

    var operation = SelectOperation(data[2].Replace("Operation: new = old", string.Empty).Replace(" ", string.Empty));

    var testValue = int.Parse(data[3].Replace("Test: divisible by", string.Empty).Replace(" ", string.Empty));
    var ifTestTrueThrow = int.Parse(data[4].Replace("If true: throw to monkey", string.Empty).Replace(" ", string.Empty));
    var ifTestFalseThrow = int.Parse(data[5].Replace("If false: throw to monkey", string.Empty).Replace(" ", string.Empty));

    return new Monkey(new Queue<ulong>(items), operation, testValue, ifTestTrueThrow, ifTestFalseThrow);
}

(Operator Operator, ulong Value) SelectOperation(string input)
{
    if (!ulong.TryParse(input[1..], out var value))
        return (Operator.Exponentiation, 2);

    if (input.StartsWith("*"))
        return (Operator.Multiplication, value);

    return (Operator.Addition, value);
}

public class Monkey
{
    private int countThrows = 0; //count each throws

    public Queue<ulong> Items { get; set; } = new Queue<ulong>();
    public (Operator Operator, ulong Value) Operation { get; set; }
    public int TestValue { get; set; }
    public int IfTestTrueThrow { get; set; }
    public int IfTestFalseThrow { get; set; }

    public Monkey(Queue<ulong> items, (Operator Operator, ulong Value) operation, int testValue, int ifTestTrueThrow, int ifTestFalseThrow)
    {
        Items = items;
        Operation = operation;
        TestValue = testValue;
        IfTestTrueThrow = ifTestTrueThrow;
        IfTestFalseThrow = ifTestFalseThrow;
    }

    public int GetCountThrows() => countThrows;

    public bool HasItemToThrow() => Items.Any();

    public (ulong Item, int Monkey) GetNextItemToThrow(int divideBy, int commonDivisor)
    {
        countThrows++;

        var item = Items.Dequeue();
        var value = Operation.Operator switch
        {
            Operator.Addition => item + Operation.Value,
            Operator.Multiplication => item * Operation.Value,
            Operator.Exponentiation => Math.Pow(item, Operation.Value),
            _ => throw new NotImplementedException()
        };

        //Get integer number less X times
        var newItem = (ulong)(value / divideBy);

        //decrease worried level [Required to work with big numbers] about common divisor
        while (newItem > (ulong)commonDivisor)
            newItem -= (ulong)commonDivisor;

        if (newItem % (ulong)TestValue is 0)
            return (newItem, IfTestTrueThrow);

        return (newItem, IfTestFalseThrow);    
    }
}

public enum Operator
{
    Addition,
    Multiplication,
    Exponentiation
}