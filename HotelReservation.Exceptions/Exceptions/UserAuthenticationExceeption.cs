namespace HotelReservation.Exceptions.Exceptions;

public class UserAuthenticationExceeption : Exception
{
    public UserAuthenticationExceeption() : base("Dear user, please log in first!")
    {

    }
}

