using Day04_GiantSquid;

while (true)
{
    var bitArrayList = InputDataHandler.ReadDataFromFile();

    //Part 1
    int GetScore()
    {
        //foreach (var bingoNumber in bitArrayList.BingoNumbers)
        //    foreach (var bingoGrid in bitArrayList.BingoGrids)
        //        if (bingoGrid.SelectNumber(bingoNumber) && bingoGrid.HasBingo())
        //            return bingoGrid.GetScore() * bingoNumber;

        return 0;
    }

    var score = GetScore();
    Console.WriteLine($"Result Part1: {score}");

    //Part 2
    int GetScore2()
    {
        var bingoGrids = bitArrayList.BingoGrids;
        foreach (var bingoNumber in bitArrayList.BingoNumbers)
            for (int i = bingoGrids.Count - 1; i >= 0; i--) 
            {
                Globals.Test1++;
                var bingoGrid = bingoGrids[i];
                if (bingoGrid.SelectNumber(bingoNumber) && bingoGrid.HasBingo())
                {
                    bingoGrids.RemoveAt(i);
                    if (bingoGrids.Count == 0)
                        return bingoGrid.GetScore() * bingoNumber;
                }
            }

        return 0;
    }

    score = GetScore2();
    Console.WriteLine($"Count loop Grids: {Globals.Test1}");
    Console.WriteLine($"Count loop HasBingo: {Globals.Test2}");
    Console.WriteLine($"Result Part2: {score}");


    Console.ReadKey();

}