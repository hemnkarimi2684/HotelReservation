namespace HotelReservation.Exceptions.Exceptions;

public class UserNameSymbolException : Exception
{
    public UserNameSymbolException() : base("the user name cannot have any symbol")
    {

    }
}

