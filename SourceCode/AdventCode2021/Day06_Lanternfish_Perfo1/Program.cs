using Day06_Lanternfish_Perfo1.Services;
using Day06_Lanternfish_Perfo1.Models;

try
{
    Console.ForegroundColor = ConsoleColor.Blue;
    var lanterfishes = ConsoleHandler.ReadLanterfishesFromCommandLineArgs();
    if (lanterfishes.Any() is false)
        lanterfishes = ConsoleHandler.ReadLanterfishesFromCommandLine();

    var requestDays = ConsoleHandler.ReadRequestDaysCommandLineArgs();
    if (requestDays is 0)
        requestDays = ConsoleHandler.ReadRequestDaysCommandLine();
    Console.ForegroundColor = ConsoleColor.White;

    if (lanterfishes.Any() is false)
        throw new Exception("Not declared initial lanterfishes");

    if (requestDays is 0)
        throw new Exception("Not declared initial requestDays");

    var lanterfishesHandler = new LanterfishesHandlerPro();
    lanterfishesHandler.Run(lanterfishes, requestDays);
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(ex.Message);
    Console.ForegroundColor = ConsoleColor.White;
}
finally
{
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("Press key to finish...");
    Console.ReadKey();
    Console.ForegroundColor = ConsoleColor.White;
}