using HotelReservation.Domain.Entities;
using HotelReservation.Infrastructure.GenericRepositories;

namespace HotelReservation.Infrastructure.RepositoriesInterFace;

public interface IReservationRepository : IGenericRepository<Reservation>
{
    List<Reservation> GetAllActives();

    bool RemoveByRoomId(Guid roomId);
}
