using HotelReservation.Infrastructure.GenericJsonRepositories;
using HotelReservation.Infrastructure.GenericRepositories;
using HotelReservation.Infrastructure.RepositoriesInterFace;
using HotelResrrvation.Domain.Entities;

namespace HotelReservation.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(IJsonRepository<User> jsonRepository) : base(jsonRepository)
    {
    }

    public User? GetByUserName(string userName)
    {
        _entities = _jsonRepository.Read();

        User? user = null;

        foreach (var item in _entities)
        {
            if (item.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase))
            {
                user = item;
                break;
            }
        }

        return user;
    }
    public bool HasDuplicateUserName(string userName)
    {
        _entities = _jsonRepository.Read();

        foreach (var item in _entities)
        {
            if (item.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    public List<User> GetAllActives()
    {
        _entities = _jsonRepository.Read();

        var users = new List<User>();

        foreach (var user in _entities)
        {
            if (user.DeletedAt is null)
            {
                users.Add(user);
            }
        }

        return users;
    }
}
