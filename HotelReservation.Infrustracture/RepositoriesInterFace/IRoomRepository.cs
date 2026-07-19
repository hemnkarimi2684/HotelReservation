using HotelReservation.Domain.Entities;
using HotelReservation.Infrastructure.GenericRepositories;

namespace HotelReservation.Infrastructure.RepositoriesInterFace;

public interface IRoomRepository : IGenericRepository<Room>
{
    bool HasDuplicateRoomNumber(int roomNumber);

    List<Room> GetAllActives();

    Room? GetByRoomNumber(int roomNumber);
}
