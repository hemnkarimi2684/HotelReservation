using HotelReservation.Business.Authentication;
using HotelReservation.Business.ServiceInterFaces;
using HotelReservation.Exceptions.Exceptions;
using HotelReservation.Presentation.Loggers;
using HotelResrrvation.Domain.Enums;

namespace HotelReservation.Presentation.Menus;

public class MainMenu
{
    private readonly IUserService _userService;

    private readonly IRoomService _roomService;

    private readonly IReservationService _reservationService;

    private readonly ILogger _logger;

    public MainMenu(IUserService userService, IRoomService roomService, IReservationService reservationService, ILogger logger)
    {
        _userService = userService;
        _roomService = roomService;
        _reservationService = reservationService;
        _logger = logger;
    }

    public void Run()
    {
        var currentUser = UserAuthentication.GetAuthenticatedUser();

        if (currentUser is null)
            throw new UserAuthenticationExceeption();

        var userMenu = new UserMenu(_roomService, _reservationService, _logger);

        var adminMenu = new AdminMenu(_userService, _roomService, _reservationService, _logger);

        if (currentUser.UserRole is Role.Admin)
        {
            _logger.LogInfo("Press any key to go to the desired panel: ");
            Console.ReadKey();

            Console.Clear();

            Console.WriteLine("Do you want to continue with the admin role or the user role?");
            Console.WriteLine("[1]Admin\t[2]User");

            var consoleKey = Console.ReadKey();

            Console.WriteLine();

            if (consoleKey.Key == ConsoleKey.D1)
                adminMenu.Menu();
            
            else if (consoleKey.Key == ConsoleKey.D2)
                userMenu.Menu();
            
            else
            {
                _logger.LogError("invalid choice input!");
                ProjectStarter.Run();
            }
        }

        if (currentUser.UserRole is Role.User)
        {
            userMenu.Menu();
        }
    }

}
