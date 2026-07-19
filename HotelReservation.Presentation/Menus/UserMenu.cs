using HotelReservation.Business.Authentication;
using HotelReservation.Business.ServiceInterFaces;
using HotelReservation.Domain.Enums;
using HotelReservation.Exceptions.Exceptions;
using HotelReservation.Extensions.BusinessExtensions;
using HotelReservation.Presentation.Loggers;
using HotelReservationSystem.Business.Contracts;
using System.Text;

namespace HotelReservation.Presentation.Menus;

public class UserMenu
{
    private readonly IRoomService _roomService;

    private readonly IReservationService _reservationService;

    private readonly ILogger _logger;

    public UserMenu(IRoomService roomService, IReservationService reservationService, ILogger logger)
    {
        _roomService = roomService;
        _reservationService = reservationService;
        _logger = logger;
    }

    public void Menu()
    {
        _logger.LogInfo("Press any key to go to the desired panel: ");
        Console.ReadKey();

        Console.Clear();

        var currentUser = UserAuthentication.GetAuthenticatedUser();

        if (currentUser is null)
            throw new UserAuthenticationExceeption();

        _logger.LogSuccess("Dear user, welcome to the hotel website:)");

        Console.WriteLine();

        _logger.LogInfo("[1]View the list of available rooms\t[2]Room Reservation");
        _logger.LogInfo("[3]View your reservation list\t[4]Cancel Reservation\t[5]Logout");

        Console.WriteLine();

        _logger.LogInfo("Please enter your desired option.");
        var key = Console.ReadKey();

        Console.WriteLine();

        switch (key.Key)
        {
            case ConsoleKey.D1:
                ShowAvailableRooms();
                break;
            case ConsoleKey.D2:
                RoomReservation();
                break;
            case ConsoleKey.D3:
                ViewYourReservation();
                break;
            case ConsoleKey.D4:
                CancelReservation();
                break;
            case ConsoleKey.D5:
                LogOut();
                break;
            default:
                _logger.LogError("invalid key input!");
                Menu();
                break;
        }

        Menu(); 
    }

    private string GetStringInput(string message)
    {
        var input = GetInput(message);

        if (string.IsNullOrWhiteSpace(input))
            throw new InvalidInputException("invalid string input!");

        return input;
    }

    private string? GetInput(string message)
    {
        Console.Write(message);
        var input = Console.ReadLine();

        return input;
    }

    private int GetIntInput(string message)
    {
        var input = GetInput(message);

        var isInteger = int.TryParse(input, out var integer);

        if (!isInteger || integer < 0)
            throw new InvalidInputException("invalid int input!");

        return integer;
    }

    private void ShowAvailableRooms()
    {
        _logger.LogInfo("Press any key to go to the desired panel");
        Console.ReadKey();

        Console.Clear();

        var rooms = _roomService.GetAllActives();

        var info = new StringBuilder();

        foreach (var room in rooms)
        {
            info.AppendLine($"[RoomNumber: {room.RoomNumber} | TypeOfRoom: {room.TypeOfRoom} | RoomCapacity: {room.Capacity} \n| RoomPricePerNight: {room.PricePerNight}]\n");
        }

        Console.WriteLine(info.ToString());

        Console.WriteLine();

        Console.Write("Dear user, would you like to reserve a room?(Y/N) ");

        var choice = Console.ReadKey();

        Console.WriteLine();

        if (choice.Key is ConsoleKey.Y)
        {
            RoomReservation();
        }
        else if (choice.Key is ConsoleKey.N)
        {
            Menu();
        }
        else
        {
            _logger.LogError("invalid choice!");
            ShowAvailableRooms();
        }
    }

    private void RoomReservation()
    {
        _logger.LogInfo("Press any key to go to the desired panel");
        Console.ReadKey();

        Console.Clear();

        var currentUser = UserAuthentication.GetAuthenticatedUser();

        if (currentUser is null)
            throw new UserAuthenticationExceeption();

        _logger.LogInfo("Dear user, welcome to reserve panel:)");

        Console.WriteLine();

        var roomNumber = GetIntInput("please enter the number of room you want to reserve: ");

        var room = _roomService.GetByRoomNumber(roomNumber);

        var checkInDate = GetDateTime("Please enter your check-in date in the following order: year, month, day. ");
        var checkOutDate = GetDateTime("Please enter your check-out date in the following order: year, month, day. ");

        var request = new AddReservationRequest()
        {
            RoomId = room.Id,
            UserId = currentUser.Id,
            Check_inDate = checkInDate,
            Check_OutDate = checkOutDate
        };

        _reservationService.Create(request);

        _logger.LogSuccess("Your reservation has been successfully registered. Thank you for your activity :)");
    }

    private DateTime GetDateTime(string message)
    {
        message.ToTitleCase();

        var year = GetIntInput("Please enter the year: ");
        var month = GetIntInput("Please enter the month: ");
        var day = GetIntInput("Please enter the day: ");

        var isDateTime = DateTime.TryParse($"{year}-{month}-{day}", out var dateTime);

        if (!isDateTime)
            throw new InvalidDateTimeException();

        return dateTime;
    }

    private void ViewYourReservation()
    {
        _logger.LogInfo("Press any key to go to the desired panel");
        Console.ReadKey();

        Console.Clear();

        var currentUser = UserAuthentication.GetAuthenticatedUser();

        if (currentUser is null)
            throw new UserAuthenticationExceeption();

        var reservations = _reservationService.GetByUserId(currentUser.Id);

        if (reservations.Count == 0)
            throw new UserReservationNotFoundException();

        var info = new StringBuilder();

        foreach (var reserve in reservations)
        {
            var room = _roomService.GetById(reserve.RoomId);

            info.AppendLine($"[RoomNumber: {room.RoomNumber} | TypeOfRoom: {room.TypeOfRoom} | reserveStatus: {reserve.Status}");
            info.AppendLine($"| CheckedInDate: {reserve.Check_inDate} | CheckedOutDate: {reserve.Check_OutDate}");
            info.AppendLine($"| TotalPrice: {reserve.TotalPrice}]");
            info.AppendLine();
        }

        Console.WriteLine(info.ToString());
    }

    private void CancelReservation()
    {
        _logger.LogInfo("Press any key to go to the desired panel");
        Console.ReadKey();

        Console.Clear();

        var currentUser = UserAuthentication.GetAuthenticatedUser();

        if (currentUser is null)
            throw new UserAuthenticationExceeption();

        var reservations = _reservationService.GetByUserId(currentUser.Id);

        if (reservations.Count == 0)
            throw new UserReservationNotFoundException();

        var info = new StringBuilder();

        foreach (var reserve in reservations)
        {
            info.AppendLine($"[ReservationId: {reserve.Id} | CheckedInDate: {reserve.Check_inDate} \n| CheckedOutDate: {reserve.Check_OutDate} | TotalPrice: {reserve.TotalPrice}]\n");
        }

        Console.WriteLine(info.ToString());

        var input = GetStringInput("Please enter the ID of the reservation you want: ");
        var isReservationId = Guid.TryParse(input, out var reservationId);

        if (!isReservationId)
            throw new InvalidInputException("invalid reservation id input!");

        var cancelReserve = _reservationService.GetById(reservationId);

        if (cancelReserve.UserId != currentUser.Id)
            throw new AccessDeniedException();

        _reservationService.UpdateReservationStatus(cancelReserve.Id, ReservationStatus.Cancelled);

        _roomService.UpdateStatus(cancelReserve.RoomId, true);

        _reservationService.Delete(cancelReserve.Id);

        _logger.LogSuccess("Your reservation has been successfully canceled.");
    }

    private void LogOut()
    {
        _logger.LogInfo("Press any key to go to the desired panel: ");
        Console.ReadKey();

        Console.Clear();

        _logger.LogInfo("Are you sure you want to log out? [1]yes\t[2]no ");
        var choice = Console.ReadKey();

        Console.WriteLine();

        if (choice.Key == ConsoleKey.D1)
        {
            UserAuthentication.SetAuthentication(null);

            ProjectStarter.Run();
        }

        else if (choice.Key == ConsoleKey.D2)
            Menu();

        else
        {
            _logger.LogError("invalid key input!");

            LogOut();
        }
    }
}
