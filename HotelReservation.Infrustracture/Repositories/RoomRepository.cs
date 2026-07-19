using HotelReservation.Domain.Entities;
using HotelReservation.Infrastructure.GenericJsonRepositories;
using HotelReservation.Infrastructure.GenericRepositories;
using HotelReservation.Infrastructure.RepositoriesInterFace;

namespace HotelReservation.Infrastructure.Repositories;

public class RoomRepository : GenericRepository<Room>, IRoomRepository
{
    public RoomRepository(IJsonRepository<Room> jsonRepository) : base(jsonRepository)
    {
    }

    public bool HasDuplicateRoomNumber(int roomNumber)
    {
        _entities = _jsonRepository.Read();

        foreach (var room in _entities)
        {
            if (room.RoomNumber == roomNumber && room.DeletedAt is null)
            {
                return true;
            }
        }

        return false;
    }

    public List<Room> GetAllActives()
    {
        _entities = _jsonRepository.Read();

        var rooms = new List<Room>();

        foreach (var room in _entities)
        {
            if (room.DeletedAt is null && room.IsAvailable is true)
            {
                rooms.Add(room);
            }
        }

        return rooms;
    }

    public Room? GetByRoomNumber(int roomNumber)
    {
        _entities = _jsonRepository.Read();

        foreach (var room in _entities)
        {
            if (room.RoomNumber == roomNumber)
            {
                return room;
            }
        }

        return null;
    }
}
