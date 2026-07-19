namespace HotelReservation.Exceptions.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException() : base("user with this id not found!")
    {

    }
}

