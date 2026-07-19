namespace HotelReservation.Exceptions.Exceptions;

public class PhoneNumberMatchesUsernameException : Exception
{
    public PhoneNumberMatchesUsernameException() : base("Invalid user name! user name cannot be equals with your phone number")
    {

    }
}

