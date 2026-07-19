namespace HotelReservation.Exceptions.Exceptions;

public class InvalidCredentialsException : Exception
{
    public InvalidCredentialsException() : base("Your Password/UserName is invalid! please try again")
    {

    }
}

