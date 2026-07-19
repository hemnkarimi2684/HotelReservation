using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Enums;
using HotelReservation.Infrastructure.GenericJsonRepositories;
using HotelReservation.Infrastructure.GenericRepositories;

namespace HotelReservation.Infrastructure.RepositoriesInterFace;

public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
{
    public ReservationRepository(IJsonRepository<Reservation> jsonRepository) : base(jsonRepository) { }
    
    public List<Reservation> GetAllActives()
    {
        _entities = _jsonRepository.Read();

        var reservations = new List<Reservation>();

        foreach (var reservation in _entities)
        {
            if (reservation.DeletedAt is null && reservation.Status != ReservationStatus.Cancelled)
            {
                reservations.Add(reservation);
            }
        }

        return reservations;
    }

    public bool RemoveByRoomId(Guid roomId)
    {
        _entities = _jsonRepository.Read();

        foreach (var reservation in _entities)
        {
            if (reservation.RoomId == roomId)
            {
                return Delete(reservation.Id);
            }
        }

        return false;
    }
}
