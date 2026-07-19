using HotelReservation.Business.Contracts;
using HotelReservation.Business.ServiceInterFaces;
using HotelReservation.Exceptions.Exceptions;
using HotelReservation.Extensions.BusinessExtensions;
using HotelReservation.Infrastructure.RepositoriesInterFace;
using HotelResrrvation.Domain.Entities;
using HotelResrrvation.Domain.Enums;

namespace HotelReservation.Business.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public bool Delete(Guid id)
    {
        var result = _userRepository.Delete(id);

        if (result is false)
            throw new UserNotFoundException();

        return result;
    }

    public List<User> GetAll()
    {
        var users = _userRepository.GetAll();

        return users;
    }

    public List<User> GetAllActives()
    {
        var users = _userRepository.GetAllActives();

        return users;
    }

    public User GetById(Guid id)
    {
        var user = _userRepository.GetById(id, true);

        if (user is null)
            throw new UserNotFoundException();

        return user;
    }

    public User Login(string userName, string password)
    {
        var user = _userRepository.GetByUserName(userName);

        if (user is null)
            throw new InvalidCredentialsException();

        if (user.Passwrod != password)
            throw new InvalidCredentialsException();

        return user;
    }

    public void Register(AddUserRequest addUserRequest)
    {
        var result = _userRepository.HasDuplicateUserName(addUserRequest.UserName);

        if (result)
            throw new DuplicateUserNameException();

        var user = new User(addUserRequest.FirstName, addUserRequest.LastName, addUserRequest.UserName, addUserRequest.Passwrod, addUserRequest.UserRole, addUserRequest.PhoneNumber);

        _userRepository.Add(user);
    }

    public void SeedUsers()
    {
        var admin = new User("hemen", "karimi", "hemen2684", "Hemen@2684", Role.Admin, "+989305674517");

        var findAdmin = _userRepository.GetByUserName(admin.UserName);

        if (findAdmin is not null)
            return;

        _userRepository.Add(admin);
    }

    public bool Update(Guid id, string firstName, string lastName, string phoneNumber)
    {
        var user = GetById(id);

        ValidateForUpdate(firstName, lastName, phoneNumber);

        user.UpdateUser(firstName, lastName, phoneNumber);

        var result = _userRepository.Update(user);

        if (result is false)
            throw new UserNotFoundException();

        return result;
    }

    private void ValidateForUpdate(string firstName, string lastName, string phoneNumber)
    {
        if (firstName.IsValidText())
            throw new ArgumentNullException("Your first name cannot be empty or less than 3 characters.");

        if(lastName.IsValidText())
            throw new ArgumentNullException("Your last name cannot be empty or less than 3 characters.");

        phoneNumber.IsValidPhoneNumber();
    }
}
