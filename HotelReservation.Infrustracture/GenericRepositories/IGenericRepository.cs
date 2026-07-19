using HotelReservation.Domain.Entities;

namespace HotelReservation.Infrastructure.GenericRepositories;

public interface IGenericRepository<T> where T : BaseEntity
{
    void Add(T entity);

    bool Update(T entity);

    bool Delete(Guid id);

    T? GetById(Guid id, bool ignoreDeleted);

    List<T> GetAll();
}
