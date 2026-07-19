namespace HotelReservation.Exceptions.Exceptions;

public class AccessDeniedException : Exception
{
    public AccessDeniedException() : base("You can only cancel your own reservation.")
    {
        
    }
}
