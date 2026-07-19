using HotelReservation.Domain.Entities;

namespace HotelReservation.Extensions.BusinessExtensions;

public static class ReservationExtensions
{
    public static int GetStayNight(this Reservation reservation)
    {
        var totalNights = (reservation.Check_OutDate - reservation.Check_inDate).Days;

        return totalNights;
    }

    public static decimal CalculateTotalPrice(this Reservation reservation, Room room)
    {
        var totalNights = reservation.GetStayNight();

        var total = room.PricePerNight * totalNights;

        return total;
    }
}
