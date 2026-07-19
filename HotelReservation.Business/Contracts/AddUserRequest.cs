using HotelResrrvation.Domain.Enums;

namespace HotelReservation.Business.Contracts;

public class AddUserRequest
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string UserName { get; set; }

    public string Passwrod { get; set; }

    public Role UserRole { get; set; }

    public string PhoneNumber { get; set; }
}
