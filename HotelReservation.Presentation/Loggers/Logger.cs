namespace HotelReservation.Presentation.Loggers;

public class Logger : ILogger
{
    private void PrintMessage(string message)
    {
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public void LogError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        PrintMessage(message);
    }

    public void LogInfo(string message)
    {
        PrintMessage(message);
    }

    public void LogWarning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        PrintMessage(message);
    }

    public void LogSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        PrintMessage(message);
    }
}
