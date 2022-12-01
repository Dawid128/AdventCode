using Day03_BinaryDiagnostic;
using System.Collections;

while (true)
{
    var bitArrayList = InputDataHandler.ReadDataFromFile();

    //Part 1
    var count = bitArrayList.First().Length;
    var countHalfRecords = bitArrayList.Count / 2;
    var gammaRate = new bool[count];
    var epsilonRate = new bool[count];

    for (int i = 0; i < count; i++)
    {
        var countTrue = bitArrayList.Select(x => x[i]).Count(x => x == true);

        var temp = (countTrue >= countHalfRecords);
        gammaRate[i] = temp;
        epsilonRate[i] = !temp;
    }

    var tempInt = new int[1];
    new BitArray(gammaRate.Reverse().ToArray()).CopyTo(tempInt, 0);
    var gammaRateInt = tempInt[0];

    tempInt = new int[1];
    new BitArray(epsilonRate.Reverse().ToArray()).CopyTo(tempInt, 0);
    var epsilonRateInt = tempInt[0];

    Console.WriteLine($"Result Part1: {gammaRateInt * epsilonRateInt}");

    //Part 2

    var oxygenRation = RatingMath.FindOxygenRating(bitArrayList.ToList());
    var carbonDioxide = RatingMath.FindCarbonDioxideRating(bitArrayList.ToList());

    Console.WriteLine($"Result Part2: {oxygenRation * carbonDioxide}");


    Console.ReadKey();

}

public static class RatingMath
{
    public static int FindOxygenRating(List<bool[]> bitArrayList)
    {
        var count = bitArrayList.First().Length;
        for (int i = 0; i < count; i++)
        {
            bitArrayList = FindOxygenRatingCandidates(bitArrayList, i);
            if (bitArrayList.Count == 1)
                break;
        }

        var tempInt = new int[1];
        new BitArray(bitArrayList.First().Reverse().ToArray()).CopyTo(tempInt, 0);
        return tempInt[0];
    }

    private static List<bool[]> FindOxygenRatingCandidates(List<bool[]> bitArrayList, int position)
    {
        var countHalfRecords = (double)bitArrayList.Count / (double)2;
        var countTrue = bitArrayList.Select(x => x[position]).Count(x => x == true);

        var temp = (countTrue >= countHalfRecords);
        return bitArrayList.Where(x => x[position] == temp).ToList();
    }

    public static int FindCarbonDioxideRating(List<bool[]> bitArrayList)
    {
        var count = bitArrayList.First().Length;
        for (int i = 0; i < count; i++)
        {
            bitArrayList = FindCarbonDioxideRatingCandidates(bitArrayList, i);
            if (bitArrayList.Count == 1)
                break;
        }

        var tempInt = new int[1];
        new BitArray(bitArrayList.First().Reverse().ToArray()).CopyTo(tempInt, 0);
        return tempInt[0];
    }

    private static List<bool[]> FindCarbonDioxideRatingCandidates(List<bool[]> bitArrayList, int position)
    {
        var countHalfRecords = (double)bitArrayList.Count / (double)2;
        var countTrue = bitArrayList.Select(x => x[position]).Count(x => x == true);

        var temp = (countTrue < countHalfRecords);
        return bitArrayList.Where(x => x[position] == temp).ToList();
    }
}