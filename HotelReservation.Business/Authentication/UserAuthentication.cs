using HotelResrrvation.Domain.Entities;

namespace HotelReservation.Business.Authentication;

public static class UserAuthentication
{
    private static User? _currentUser = null;

    public static void SetAuthentication(User user)
    {
        _currentUser = user;
    }

    public static User? GetAuthenticatedUser()
    {
        return _currentUser;
    }

    public static bool IsUserAuthenticated()
    {
        return _currentUser is not null;
    }
}
