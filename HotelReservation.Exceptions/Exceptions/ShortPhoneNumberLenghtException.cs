namespace HotelReservation.Exceptions.Exceptions;

public class ShortPhoneNumberLenghtException : Exception
{
    public ShortPhoneNumberLenghtException() : base("the phone number cannot be less than 11 character")
    {

    }
}

