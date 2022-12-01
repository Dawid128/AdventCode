using Day01_SonarSweep.Services;

while (true)
{
    var depthRecords = InputDataHandler.ReadDataFromFile();

    //Part 1
    var count = 0;
    for (int i = 1; i < depthRecords.Count; i++)
        if (depthRecords[i] > depthRecords[i - 1])
            count++;

    Console.WriteLine($"Result Part1: {count}");

    //Part 2

    var depthSumRecords = new List<int>();
    for (int i = 0; i < depthRecords.Count - 2; i++)
        depthSumRecords.Add(depthRecords[i] + depthRecords[i + 1] + depthRecords[i + 2]);

    count = 0;
    for (int i = 1; i < depthSumRecords.Count; i++)
        if (depthSumRecords[i] > depthSumRecords[i - 1])
            count++;

    Console.WriteLine($"Result Part2: {count}");
}