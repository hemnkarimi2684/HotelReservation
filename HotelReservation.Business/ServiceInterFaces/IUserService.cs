using HotelReservation.Business.Contracts;
using HotelResrrvation.Domain.Entities;

namespace HotelReservation.Business.ServiceInterFaces;

public interface IUserService
{
    /// <summary>
    /// لاگین کاربر در سیستم
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    User Login(string userName, string password);

    /// <summary>
    /// ثبت نام کاربر در سیستم 
    /// </summary>
    /// <param name="addUserRequest"></param>
    void Register(AddUserRequest addUserRequest);

    /// <summary>
    /// سافت دیلیت کاربر در سیستم 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    bool Delete(Guid id);

    /// <summary>
    /// اپدیت اطلاعات یک کاربر در سیستم 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="phoneNumber"></param>
    /// <returns></returns>
    bool Update(Guid id,string firstName, string lastName, string phoneNumber);

    /// <summary>
    /// گرفتن یک کاربر توسط ایدیش
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    User GetById(Guid id);

    /// <summary>
    /// گرفتن تمام کاربر های موجود و غیر موجود و حذف شده 
    /// </summary>
    /// <returns></returns>
    List<User> GetAll();

    /// <summary>
    /// گرفتن کاربر های فعال و حذف نشده 
    /// </summary>
    /// <returns></returns>
    List<User> GetAllActives();

    /// <summary>
    /// ثبت ادمین در سیستم 
    /// </summary>
    void SeedUsers();
}
