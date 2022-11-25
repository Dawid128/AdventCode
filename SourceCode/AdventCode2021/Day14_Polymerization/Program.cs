using Day14_Polymerization.Services;

var data = Environment.GetCommandLineArgs();

while(true)
{
    Console.Clear();
    int days = 0;
    if (data.Length >= 2)
        days = int.Parse(data[1]);
    else
    {
        Console.Write("Days: ");
        days = int.Parse(Console.ReadLine());
    }

    int exampleNumber = 1;
    if (data.Length >= 3)
        exampleNumber = int.Parse(data[2]);
    else
    {
        Console.Write("Example Number: ");
        exampleNumber = int.Parse(Console.ReadLine());
    }

    var content = File.ReadAllLines($"Resources\\Example{exampleNumber}.txt");
    var inputPolymer = content[0];
    var pairPolymers = content.Skip(1).Select(x => x.Split(" -> ")).ToDictionary(x => x[0], y => char.Parse(y[1]));

    PolymerizationHandlerPro.Run(inputPolymer, pairPolymers, days);
}