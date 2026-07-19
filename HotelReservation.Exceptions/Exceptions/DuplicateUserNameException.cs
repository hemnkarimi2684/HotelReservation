namespace HotelReservation.Exceptions.Exceptions;

public class DuplicateUserNameException : Exception
{
    public DuplicateUserNameException() : base("This username already exists. Please try another username.")
    {

    }
}

