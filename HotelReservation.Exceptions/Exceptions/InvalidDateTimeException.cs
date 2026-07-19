namespace HotelReservation.Exceptions.Exceptions;

public class InvalidDateTimeException : Exception
{
    public InvalidDateTimeException() : base("invalid DateTime input!")
    {

    }
}

