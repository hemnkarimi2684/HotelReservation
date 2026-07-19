using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Enums;

namespace HotelReservation.Business.ServiceInterFaces;

public interface IRoomService
{
    /// <summary>
    /// ساخت یک اتاق در سیستم 
    /// </summary>
    /// <param name="roomNumber"></param>
    /// <param name="typeOfRoom"></param>
    /// <param name="pricePerNight"></param>
    /// <param name="isAvailable"></param>
    void Create(int roomNumber, RoomType typeOfRoom, decimal pricePerNight, bool isAvailable);

    /// <summary>
    /// سافت دیلیت کردن یک اتاق در سیستم 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    bool Delete(Guid id);

    /// <summary>
    /// آپدیت قیمت و وضعیت در دسترس بودن اتاق
    /// </summary>
    /// <param name="id"></param>
    /// <param name="pricePerNight"></param>
    /// <param name="isAvailable"></param>
    /// <returns></returns>
    bool Update(Guid id, decimal pricePerNight, bool isAvailable);

    /// <summary>
    /// گرفتن اتاق توسط ایدیش
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Room GetById(Guid id);

    /// <summary>
    /// گرفتن تمام اتاق های موجود و غیر موجود و حذف شده
    /// </summary>
    /// <returns></returns>
    List<Room> GetAll();

    /// <summary>
    /// گرفتن تمام اتاق های در دسترس و حذف نشده 
    /// </summary>
    /// <returns></returns>
    List<Room> GetAllActives();

    /// <summary>
    /// ثبت اتاق های موجود در سیستم 
    /// </summary>
    void SeedRooms();

    /// <summary>
    /// دریافت یک اتاق توسط شماره اتاقش
    /// </summary>
    /// <param name="roomNumber"></param>
    /// <returns></returns>
    Room GetByRoomNumber(int roomNumber);

    /// <summary>
    /// اپدیت وضعیت در دسترس بودن یک اتاق
    /// </summary>
    /// <param name="roomId"></param>
    /// <param name="isAvailable"></param>
    /// <returns></returns>
    bool UpdateStatus(Guid roomId, bool isAvailable);
}
