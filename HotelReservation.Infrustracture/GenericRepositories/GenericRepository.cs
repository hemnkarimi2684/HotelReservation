using HotelReservation.Domain.Entities;
using HotelReservation.Infrastructure.GenericJsonRepositories;

namespace HotelReservation.Infrastructure.GenericRepositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected List<T> _entities = new();

    protected readonly IJsonRepository<T> _jsonRepository;

    public GenericRepository(IJsonRepository<T> jsonRepository)
    {
        _jsonRepository = jsonRepository;
    }

    public void Add(T entity)
    {
        _entities = _jsonRepository.Read();

        _entities.Add(entity);

        _jsonRepository.Save(_entities);
    }

    public bool Delete(Guid id)
    {
        var entity = GetById(id, true);

        if (entity is null)
            return false;

        entity.SetDeletedAt(DateTime.UtcNow);

        _jsonRepository.Save(_entities);

        return true;
    }

    public List<T> GetAll()
    {
        return new List<T>(_entities);
    }

    public T? GetById(Guid id, bool ignoreDeleted)
    {
        _entities = _jsonRepository.Read();

        foreach (var entity in _entities)
        {
            if (entity.Id.Equals(id) && (!ignoreDeleted || entity.DeletedAt is null))
            {
                return entity;
            }
        }

        return null;
    }

    public bool Update(T entity)
    {
        var oldEntity = GetById(entity.Id, true);

        if (oldEntity is null)
            return false;

        _entities.Remove(oldEntity);

        _entities.Add(entity);

        _jsonRepository.Save(_entities);

        return true;
    }
}
