using HotelReservation.Domain.Enums;

namespace HotelReservation.Domain.Entities;

public class Reservation : BaseEntity
{
    public Guid UserId { get; set; }

    public Guid RoomId { get; set; }

    public DateTime Check_inDate { get; set; }

    public DateTime Check_OutDate { get; set; }

    public decimal TotalPrice { get; set; } = 0;

    public ReservationStatus Status { get; set; }


    public Reservation(Guid userId, Guid roomId, DateTime check_inDate, DateTime check_OutDate, ReservationStatus status)
    {
        UserId = userId;

        RoomId = roomId;

        Check_inDate = check_inDate;

        Check_OutDate = check_OutDate;

        Status = status;

        Validate();
    }

    protected override void Validate()
    {
        if (Check_inDate < DateTime.UtcNow || Check_OutDate < DateTime.UtcNow)
            throw new InvalidTimeZoneException("invalid DateTime! enter or exit time cannot be lower than now date time.");

        if (Check_inDate >= Check_OutDate)
            throw new InvalidTimeZoneException("invalid DateTime! enter time cannot be higher or equals with exit time.");

        if ((Check_OutDate - Check_inDate).Days > 30)
            throw new InvalidTimeZoneException("invalid DateTime! The maximum length of stay is 30 days.");
    }

    public void UpdateReservationStatus(ReservationStatus status)
    {
        Status = status;
        ModifiedAt = DateTime.UtcNow;
    }

    public void SetTotalPrice(decimal totalPrice)
    {
        TotalPrice = totalPrice;

        if (TotalPrice < 1)
            throw new ArgumentOutOfRangeException("Invalid Total Price!");
    }
}
