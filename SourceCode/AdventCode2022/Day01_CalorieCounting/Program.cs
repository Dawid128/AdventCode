using Day01_CalorieCounting;

var caloriesList = InputDataHandler.ReadDataFromFile();

//Part 1
Console.WriteLine($"Result Part1: {caloriesList.First()}");

//Part 2
Console.WriteLine($"Result Part2: {caloriesList.Take(3).Sum()}");