namespace HotelReservation.Exceptions.Exceptions;

public class NotAvailableRoomException : Exception
{
    public NotAvailableRoomException() : base("the selected room is not available for use")
    {

    }
}

