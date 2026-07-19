using HotelReservation.Business.ServiceInterFaces;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Enums;
using HotelReservation.Exceptions.Exceptions;
using HotelReservation.Infrastructure.RepositoriesInterFace;

namespace HotelReservation.Business.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;

    public RoomService(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public void Create(int roomNumber, RoomType typeOfRoom, decimal pricePerNight, bool isAvailable)
    {
        var hasDuplicate = _roomRepository.HasDuplicateRoomNumber(roomNumber);

        if (hasDuplicate)
            throw new DuplicateRoomNumberException();

        var room = new Room(roomNumber, typeOfRoom, pricePerNight, isAvailable);

        _roomRepository.Add(room);
    }

    public bool Delete(Guid id)
    {
        var result = _roomRepository.Delete(id);

        if (result is false)
            throw new RoomNotFoundException();

        return result;
    }

    public List<Room> GetAll()
    {
        var rooms = _roomRepository.GetAll();

        return rooms;
    }

    public List<Room> GetAllActives()
    {
        var rooms = _roomRepository.GetAllActives();

        return rooms;
    }

    public Room GetById(Guid id)
    {
        var room = _roomRepository.GetById(id, true);

        if (room is null)
            throw new RoomNotFoundException();

        return room;
    }

    public Room GetByRoomNumber(int roomNumber)
    {
        var room = _roomRepository.GetByRoomNumber(roomNumber);

        if (room is null)
            throw new RoomNotFoundException();

        return room;
    }

    public void SeedRooms()
    {
        var rooms = new List<Room>()
        {
            new Room(1,RoomType.SingleRoom,600_000,true),

            new Room(2,RoomType.DoubleRoom,800_000,true)
        };

        foreach (var room in rooms)
        {
            var existingRoom = _roomRepository.GetByRoomNumber(room.RoomNumber);

            if (existingRoom is null)
                _roomRepository.Add(room);

            else if (existingRoom.DeletedAt is not null)
            {
                existingRoom.SetDeletedAt();

                _roomRepository.Update(existingRoom);
            }
        }
    }

    public bool Update(Guid id, decimal pricePerNight, bool isAvailable)
    {
        var room = GetById(id);

        ValidateForUpdate(pricePerNight);

        room.UpdateRoom(pricePerNight, isAvailable);

        var result = _roomRepository.Update(room);

        if (result is false)
            throw new RoomNotFoundException();

        return result;
    }

    public bool UpdateStatus(Guid roomId, bool isAvailable)
    {
        var room = GetById(roomId);

        room.UpdateRoomStatus(isAvailable);

        var result = _roomRepository.Update(room);

        if (result is false)
            throw new RoomNotFoundException();

        return result;
    }

    private void ValidateForUpdate(decimal pricePerNight)
    {
        if (pricePerNight < 1)
            throw new ArgumentOutOfRangeException("invalid PricePerNight Number!");
    }
}
