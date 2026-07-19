using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Enums;
using HotelReservationSystem.Business.Contracts;

namespace HotelReservation.Business.ServiceInterFaces;

public interface IReservationService
{
    /// <summary>
    /// ساخت یک رزرو
    /// </summary>
    /// <param name="addReservation"></param>
    void Create(AddReservationRequest addReservation);

    /// <summary>
    /// سافت دیلیت یک رزرو در سیستم
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    bool Delete(Guid id);

    /// <summary>
    /// دریافت یک رزرو توسط ایدیش
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Reservation GetById(Guid id);

    /// <summary>
    ///دریافت یک رزرو توسط ایدی کاربر دارای ان رزرو
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    List<Reservation> GetByUserId(Guid userId);

    /// <summary>
    /// گرفتن همه رزرو ها با رزور های دیلیت شده
    /// </summary>
    /// <returns></returns>
    List<Reservation> GetAll();

    /// <summary>
    /// گرفتن همه رزرو های فعال,حذف نشده ها و کنسل نشده ها 
    /// </summary>
    /// <returns></returns>
    List<Reservation> GetAllActives();

    /// <summary>
    /// آپدیت وضعیت رزور 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="reservationStatus"></param>
    void UpdateReservationStatus(Guid id, ReservationStatus reservationStatus);
    
    /// <summary>
    /// حذف رزرو توسط ایدی اتاق رزور شده
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns></returns>
    bool RemoveByRoomId(Guid roomId);
}

