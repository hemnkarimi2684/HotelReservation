namespace HotelReservation.Exceptions.Exceptions;

public class UserReservationNotFoundException : Exception
{
    public UserReservationNotFoundException() : base("Dear user, there is no reservation registered in the system under your name.")
    {

    }
}

