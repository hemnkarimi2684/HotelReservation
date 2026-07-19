namespace HotelReservation.Domain.PasswordPolicy.PasswordExceptions;

public class PasswordSymbolException : Exception
{
    public PasswordSymbolException(string message) : base(message)
    {
        
    }
}
