namespace HotelReservation.Presentation.Loggers;

public interface ILogger
{
    void LogError(string message);
    void LogInfo(string message);
    void LogWarning(string message);
    void LogSuccess(string message);
}
