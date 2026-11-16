namespace FristAgentDemo.PrintHelper;

public static class Print
{
    public static void PrintHelper(string input)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(input);
        Console.ResetColor();
    }
}