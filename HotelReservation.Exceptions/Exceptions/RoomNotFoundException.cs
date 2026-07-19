namespace HotelReservation.Exceptions.Exceptions;

public class RoomNotFoundException : Exception
{
    public RoomNotFoundException() : base("room with this id not found!")
    {

    }
}

