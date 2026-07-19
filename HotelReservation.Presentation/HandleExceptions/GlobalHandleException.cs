using HotelReservation.Presentation.Loggers;
using HotelReservation.Presentation.Menus;

namespace HotelReservation.Presentation.HandleExceptions;

public static class GlobalHandleException
{
    public static ILogger _logger = new Logger();
    public static void HandleException(Exception exception)
    {
        Console.Clear();

        _logger.LogError(exception.Message);

        ProjectStarter.Run();
    }
}
