using HotelReservation.Infrastructure.GenericRepositories;
using HotelResrrvation.Domain.Entities;

namespace HotelReservation.Infrastructure.RepositoriesInterFace;

public interface IUserRepository : IGenericRepository<User>
{
    User? GetByUserName(string userName);

    bool HasDuplicateUserName(string userName);

    List<User> GetAllActives();
}
