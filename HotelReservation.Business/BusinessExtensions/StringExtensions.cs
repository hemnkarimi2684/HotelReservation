using HotelReservation.Exceptions.Exceptions;
using System.Text;

namespace HotelReservation.Extensions.BusinessExtensions;

public static class StringExtensions
{
    private static readonly string _symbols = "!@#$%^&*";

    //استفاده شده در متد اپدیت یوزر 
    public static bool IsValidText(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return false;

        if (text.Length < 3)
            return false;

        foreach (char c in text)
        {
            if (_symbols.Contains(c))
                return false;
        }

        return true;
    }

    //استفاده شده در متد اپدیت یوزر
    public static void IsValidPhoneNumber(this string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentNullException("your phone number cannot be null or empty");

        if (phoneNumber.Length < 11)
            throw new ShortPhoneNumberLenghtException();

        if (phoneNumber.StartsWith("09"))
        {
            phoneNumber = $"+98{phoneNumber.Substring(1)}";
        }
    }

    // استفاده شده در خط 186 UserMenu 
    public static void ToTitleCase(this string text)
    {
        var strings = text.Trim().Split(' ');

        var newText = new StringBuilder();

        foreach (string s in strings)
        {
            var upperChar = char.ToUpper(s[0]).ToString();

            var subText = s.Substring(1);

            newText.Append(subText.Insert(0, upperChar));
            newText.Append(" ");
        }

        Console.WriteLine(newText);
    }
}
