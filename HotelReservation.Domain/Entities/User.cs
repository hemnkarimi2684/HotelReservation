using HotelReservation.Domain.Entities;
using HotelReservation.Domain.PasswordPolicy;
using HotelReservation.Exceptions.Exceptions;
using HotelResrrvation.Domain.Enums;

namespace HotelResrrvation.Domain.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string UserName { get; set; }

    public string Passwrod { get; set; }

    public Role UserRole { get; set; }

    public string PhoneNumber { get; set; }

    public User(string firstName, string lastName, string userName, string passwrod, Role userRole, string phoneNumber)
    {
        FirstName = firstName;

        LastName = lastName;

        UserName = userName;

        Passwrod = passwrod;

        UserRole = userRole;

        PhoneNumber = phoneNumber;

        Validate();
    }

    protected override void Validate()
    {
        if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
            throw new ArgumentNullException("Your first name or last name cannot be empty.");

        if (FirstName.Length < 3 || LastName.Length < 3)
            throw new TooShortLenghtException("Your first name or last name must be at least 3 characters long.");

        ValidateUserName();

        StrongPasswordPolicy.Validate(UserName, Passwrod);

        if (string.IsNullOrWhiteSpace(PhoneNumber))
            throw new ArgumentNullException("your phone number cannot be null or empty");

        if (PhoneNumber.Length < 11)
            throw new ShortPhoneNumberLenghtException();

        if (PhoneNumber.StartsWith("09"))
        {
            PhoneNumber = $"+98{PhoneNumber.Substring(1)}";
        }
    }

    private void ValidateUserName()
    {
        const string symbols = "!@#$%&";

        if (string.IsNullOrWhiteSpace(UserName))
            throw new ArgumentNullException("Your user name cannot be empty");

        if (UserName.Length < 3 || UserName.Length > 20)
            throw new UserNameLenghtException();

        if (UserName.Equals(PhoneNumber))
            throw new PhoneNumberMatchesUsernameException();

        foreach (char c in UserName)
        {
            if (symbols.Contains(c))
            {
                throw new UserNameSymbolException();
            }
        }
    }

    public void UpdateUser(string firstName, string lastName, string phoneNumber)
    {
        FirstName = firstName;

        LastName = lastName;

        PhoneNumber = phoneNumber;

        ModifiedAt = DateTime.UtcNow;
    }
}
