using HotelReservation.Domain.Entities;

namespace HotelReservation.Infrastructure.GenericJsonRepositories;

public interface IJsonRepository<T> where T : BaseEntity
{
    void Save(List<T> values);

    List<T> Read();
}

