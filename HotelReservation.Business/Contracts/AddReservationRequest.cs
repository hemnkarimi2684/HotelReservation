namespace HotelReservationSystem.Business.Contracts;

public class AddReservationRequest
{
    public Guid UserId { get; set; }

    public Guid RoomId { get; set; }

    public DateTime Check_inDate { get; set; }

    public DateTime Check_OutDate { get; set; }
}
