using HotelReservation.Business.Authentication;
using HotelReservation.Business.ServiceInterFaces;
using HotelReservation.Business.Services;
using HotelReservation.Domain.Entities;
using HotelReservation.Infrastructure.GenericJsonRepositories;
using HotelReservation.Infrastructure.Repositories;
using HotelReservation.Infrastructure.RepositoriesInterFace;
using HotelReservation.Presentation.HandleExceptions;
using HotelReservation.Presentation.Loggers;
using HotelResrrvation.Domain.Entities;

namespace HotelReservation.Presentation.Menus;

public static class ProjectStarter
{
    private static readonly IJsonRepository<User> _jsonUserRepository = new JsonRepository<User>();
    private static readonly IJsonRepository<Room> _jsonRoomRepository = new JsonRepository<Room>();
    private static readonly IJsonRepository<Reservation> _jsonReservationRepository = new JsonRepository<Reservation>();

    private static IUserRepository _userRepository = new UserRepository(_jsonUserRepository);
    private static IRoomRepository _roomRepository = new RoomRepository(_jsonRoomRepository);
    private static IReservationRepository _reservationRepository = new ReservationRepository(_jsonReservationRepository);

    private static IUserService _userService = new UserService(_userRepository);
    private static IRoomService _roomService = new RoomService(_roomRepository);
    private static IReservationService _reservationService = new ReservationService(_reservationRepository, _roomService, _userService);

    private static ILogger _logger = new Logger();

    public static void Run()
    {
        try
        {
            _userService.SeedUsers();

            _roomService.SeedRooms();

            var login = new LoginMenu(_userService, _logger);

            if (!UserAuthentication.IsUserAuthenticated())
            {
                login.Main();
            }

            if (UserAuthentication.IsUserAuthenticated())
            {
                var mainMenu = new MainMenu(_userService, _roomService, _reservationService, _logger);

                mainMenu.Run();
            }

        }
        catch (Exception ex)
        {
            GlobalHandleException.HandleException(ex);
        }
    }
}
