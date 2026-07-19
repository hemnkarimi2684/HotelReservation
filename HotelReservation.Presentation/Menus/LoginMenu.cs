using HotelReservation.Business.Authentication;
using HotelReservation.Business.Contracts;
using HotelReservation.Business.ServiceInterFaces;
using HotelReservation.Exceptions.Exceptions;
using HotelReservation.Presentation.Loggers;
using HotelResrrvation.Domain.Enums;

namespace HotelReservation.Presentation.Menus;

public class LoginMenu
{
    private IUserService _userService;

    private ILogger _logger;

    public LoginMenu(IUserService userService, ILogger logger)
    {
        _userService = userService;
        _logger = logger;
    }

    public void Main()
    {
        _logger.LogInfo("Press any key to go to the desired panel");
        Console.ReadKey();

        Console.Clear();

        _logger.LogInfo("[1]Login\t[2]Register\t[3]Exit");

        Console.Write("enter your action: ");
        var action = Console.ReadLine();

        if (action == "1")
        {
            Login();
        }

        else if (action == "2")
        {
            Register();
        }

        else if (action == "3")
        {
            Console.Write("Are you sure you want to exit?(yes/no) ");
            var choice = Console.ReadLine().ToLower();

            if (choice == "yes")
            {

            }
            else if (choice == "no")
            {
                Main();
            }
        }

        else
        {
            _logger.LogError("invalid key input!");

            Main();
        }
    }

    private void Login()
    {
        _logger.LogInfo("Press any key to go to the desired panel");
        Console.ReadKey();

        Console.Clear();

        var userName = GetUserInput("enter your user name: ");

        var password = GetUserInput("enter your passwrod: ");

        var user = _userService.Login(userName.Trim(), password.Trim());

        UserAuthentication.SetAuthentication(user);

        _logger.LogSuccess("Dear user, you have successfully logged in.");
    }

    private void Register()
    {
        _logger.LogInfo("Press any key to go to the desired panel");
        Console.ReadKey();

        Console.Clear();

        var firstName = GetUserInput("enter your firstName: ");

        var lastName = GetUserInput("enter your lastName: ");

        var userName = GetUserInput("enter your user name: ");

        var password = GetUserInput("enter your password: ");

        var phoneNumber = GetUserInput("enter your phoneNumber: ");

        var addUser = new AddUserRequest
        {
            FirstName = firstName,
            LastName = lastName,
            UserName = userName,
            Passwrod = password,
            UserRole = Role.User,
            PhoneNumber = phoneNumber
        };

        _userService.Register(addUser);

        _logger.LogSuccess("You have successfully registered in the system. Please log in.");

        Login();
    }

    private string GetUserInput(string message)
    {
        Console.Write(message);
        var input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input) || input.Length < 3)
            throw new InvalidInputException("invalid string input!");

        return input;
    }
}
