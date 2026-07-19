namespace HotelReservation.Domain.PasswordPolicy.PasswordExceptions;

public class PasswordMissingDigitException : Exception
{
    public PasswordMissingDigitException(string message) : base(message)
    {
        
    }
}
