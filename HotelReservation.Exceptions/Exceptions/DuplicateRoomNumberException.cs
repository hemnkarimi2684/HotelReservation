namespace HotelReservation.Exceptions.Exceptions;

public class DuplicateRoomNumberException : Exception
{
    public DuplicateRoomNumberException() : base("This roomNumber already exists. Please try another roomNumber.")
    {

    }
}

