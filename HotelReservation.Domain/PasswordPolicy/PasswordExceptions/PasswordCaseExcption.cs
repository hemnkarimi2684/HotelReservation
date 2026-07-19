namespace HotelReservation.Domain.PasswordPolicy.PasswordExceptions;

public class PasswordCaseExcption : Exception
{
    public PasswordCaseExcption(string message) : base(message)
    {
        
    }
}
