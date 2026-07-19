using HotelReservation.Business.Authentication;
using HotelReservation.Business.ServiceInterFaces;
using HotelReservation.Domain.Enums;
using HotelReservation.Exceptions.Exceptions;
using HotelReservation.Presentation.Loggers;
using System.Text;

namespace HotelReservation.Presentation.Menus;

public class AdminMenu
{
    private readonly IUserService _userService;

    private readonly IRoomService _roomService;

    private readonly IReservationService _reservationService;

    private readonly ILogger _logger;

    public AdminMenu(IUserService userService, IRoomService roomService, IReservationService reservationService, ILogger logger)
    {
        _userService = userService;
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

        _logger.LogSuccess("Dear Admin, welcome to the hotel website:)");

        Console.WriteLine();

        _logger.LogInfo("[1]Add a new room to the hotel\t[2]Edit Room Information");
        _logger.LogInfo("[3]Remove room\t[4]View All Rooms\t[5]View All Reservations");
        _logger.LogInfo("[6]View All Users\t[7]LogOut");

        Console.WriteLine();

        _logger.LogInfo("Please enter your desired option.");
        var key = Console.ReadKey();

        Console.WriteLine();

        switch (key.Key)
        {
            case ConsoleKey.D1:
                AddNewRoom();
                break;
            case ConsoleKey.D2:
                UpdateRoom();
                break;
            case ConsoleKey.D3:
                RemoveRoom();
                break;
            case ConsoleKey.D4:
                ViewAllRooms();
                break;
            case ConsoleKey.D5:
                ViewAllReservations();
                break;
            case ConsoleKey.D6:
                ViewAllUsers();
                break;
            case ConsoleKey.D7:
                LogOut();
                break;
            default:
                _logger.LogError("invalid key input!");
                Menu();
                break;
        }

        Menu();
    }

    private void AddNewRoom()
    {
        var roomNumber = GetIntInput("Please enter the room number: ");

        var roomType = HandleRoomTypeInput();

        var input = GetStringInput("Please enter the Price per night for room:");
        var isDecimal = decimal.TryParse(input, out var pricePerNight);

        if (!isDecimal)
            throw new InvalidInputException("invalid price per night input!");

        var isAvailable = true;

        _roomService.Create(roomNumber, roomType, pricePerNight, isAvailable);

        _logger.LogSuccess("Your new room has been successfully registered in the system.");
    }

    private void UpdateRoom()
    {
        var rooms = _roomService.GetAllActives();

        var info = string.Empty;

        foreach (var room in rooms)
        {
            info += $"RoomNumber: {room.RoomNumber} | RoomPricePerNight: {room.PricePerNight} | RoomCapacity: {room.Capacity} | RoomStatus: {room.IsAvailable}\n";
        }

        Console.WriteLine(info);

        var input = GetStringInput("please enter the number of room you want to Update: ");
        var isRoomNumber = int.TryParse(input, out var roomNumber);

        if (!isRoomNumber)
            throw new InvalidInputException($"invalid room number input!");

        var findRoom = _roomService.GetByRoomNumber(roomNumber);

        var pricePerNightInput = GetStringInput("Please enter the price per night: ");
        var isPricePerNight = decimal.TryParse(pricePerNightInput, out var pricePerNight);

        if (!isPricePerNight)
            throw new InvalidInputException("invalid price per night input!");

        var getInput = GetStringInput("Please enter the room availability status:(True/False). ");
        var isAvailableRes = bool.TryParse(getInput, out var isAvailable);

        if (!isAvailableRes)
            throw new InvalidInputException("invalid room status input!");

        _roomService.Update(findRoom.Id, pricePerNight, isAvailable);

        _logger.LogSuccess("chosen room has been successfully updated in the system.");
    }

    private void RemoveRoom()
    {
        var rooms = _roomService.GetAllActives();

        var info = string.Empty;

        foreach (var room in rooms)
        {
            info += $"RoomId: {room.Id} | RoomNumber: {room.RoomNumber}\n";
        }

        Console.WriteLine(info);

        var roomNumber = GetIntInput("please enter the number of room you want to delete: ");

        var findRoom = _roomService.GetByRoomNumber(roomNumber);

        if (findRoom.IsAvailable is false)
            throw new ArgumentException("This room is currently reserved. Please proceed with removing it after the reservation period has ended.");

        var result = _roomService.Delete(findRoom.Id);

        if (result)
        {
            var resultOfRemove = _reservationService.RemoveByRoomId(findRoom.Id);

            if (!resultOfRemove)
                _logger.LogWarning("No reservation has been made with this room.");
        }

        _logger.LogSuccess("The selected room was successfully deleted from the system.");
    }

    private void ViewAllRooms()
    {
        var rooms = _roomService.GetAll();

        var info = new StringBuilder();

        foreach (var room in rooms)
        {
            info.AppendLine($"RoomNumber: {room.RoomNumber} | RoomDeleteAt: {room.DeletedAt}");
            info.AppendLine($"RoomPricePerNight: {room.PricePerNight} | RoomCapacity: {room.Capacity}");
            info.AppendLine($"RoomIsAvailable: {room.IsAvailable} | RoomType: {room.TypeOfRoom}");
            info.AppendLine();
        }


        Console.WriteLine(info.ToString());
    }

    private void ViewAllReservations()
    {
        var reservations = _reservationService.GetAll();

        var info = new StringBuilder();

        foreach (var reservation in reservations)
        {
            var user = _userService.GetById(reservation.UserId);

            var room = _roomService.GetById(reservation.RoomId);

            info.AppendLine($"ReservationUserName: {user.UserName} | ReservationRoomNumber: {room.RoomNumber}");
            info.AppendLine($"ReservationCheck_inDate: {reservation.Check_inDate} | ReservationCheck_outDate: {reservation.Check_OutDate}");
            info.AppendLine($"ReservationTotalPrice: {reservation.TotalPrice} | ReservationStatus: {reservation.Status}");
            info.AppendLine();
        }

        Console.WriteLine(info.ToString());
    }

    private void ViewAllUsers()
    {
        var users = _userService.GetAll();

        var info = new StringBuilder();

        foreach (var user in users)
        {
            info.AppendLine($"FullName: {user.FirstName} {user.LastName} | UserName: {user.UserName} | Passwrod: {new string('*', user.Passwrod.Length)}");
            info.AppendLine($"Role: {user.UserRole} | userDeleteAt: {user.DeletedAt} | PhoneNumber: {user.PhoneNumber}");
            info.AppendLine();
        }

        Console.WriteLine(info.ToString());
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

    private RoomType HandleRoomTypeInput()
    {
        var roomTypes = Enum.GetNames<RoomType>();

        for (var i = 1; i <= roomTypes.Length; i++)
        {
            Console.Write($"[{i}].{roomTypes[i - 1]}\t");
        }

        var choice = GetIntInput("enter the roomType: ");

        if (choice < 1 || choice > roomTypes.Length)
        {
            _logger.LogError("invalid room type input!");
            return HandleRoomTypeInput();
        }

        return Enum.Parse<RoomType>(roomTypes[choice - 1]);
    }

    private void LogOut()
    {
        _logger.LogInfo("Press any key to go to the desired panel: ");
        Console.ReadKey();

        Console.Clear();

        _logger.LogInfo("Are you sure you want to log out? [1]yes\t[2]no ");
        var choice = Console.ReadKey();

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

