namespace HotelReservation.Exceptions.Exceptions;

public class UserNameLenghtException : Exception
{
    public UserNameLenghtException() : base("Your user name must be at least 3 characters long or Shorter than 20 characters.")
    {

    }
}

