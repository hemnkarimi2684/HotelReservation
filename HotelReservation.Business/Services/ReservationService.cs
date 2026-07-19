using HotelReservation.Business.ServiceInterFaces;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Enums;
using HotelReservation.Exceptions.Exceptions;
using HotelReservation.Extensions.BusinessExtensions;
using HotelReservation.Infrastructure.RepositoriesInterFace;
using HotelReservationSystem.Business.Contracts;

namespace HotelReservation.Business.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;

    private readonly IRoomService _roomService;

    private readonly IUserService _userService;

    public ReservationService(IReservationRepository reservationRepository, IRoomService roomService, IUserService userService)
    {
        _reservationRepository = reservationRepository;
        _roomService = roomService;
        _userService = userService;
    }

    public void Create(AddReservationRequest addReservation)
    {
        var room = _roomService.GetById(addReservation.RoomId);

        if (room.IsAvailable is false)
            throw new NotAvailableRoomException();

        var user = _userService.GetById(addReservation.UserId);

        var status = ReservationStatus.Confirmed;

        var reservation = new Reservation(user.Id, room.Id, addReservation.Check_inDate, addReservation.Check_OutDate, status);

        var totalPrice = reservation.CalculateTotalPrice(room);

        reservation.SetTotalPrice(totalPrice);

        _roomService.Update(room.Id, room.PricePerNight, false);

        _reservationRepository.Add(reservation);
    }

    public bool Delete(Guid id)
    {
        var result = _reservationRepository.Delete(id);

        if (result is false)
            throw new ReservationNotFoundException("reservation with this id not found!");

        return result;
    }

    public List<Reservation> GetAll()
    {
        var reservations = _reservationRepository.GetAll();

        return reservations;
    }

    public List<Reservation> GetAllActives()
    {
        var reservations = _reservationRepository.GetAllActives();

        return reservations;
    }

    public Reservation GetById(Guid id)
    {
        var reservation = _reservationRepository.GetById(id, true);

        if (reservation is null)
            throw new ReservationNotFoundException("reservation with this id not found!");

        return reservation;
    }

    public List<Reservation> GetByUserId(Guid userId)
    {
        var reservations = GetAllActives();

        var userReserves = new List<Reservation>();

        foreach (Reservation reservation in reservations)
        {
            if (reservation.UserId == userId)
            {
                userReserves.Add(reservation);
            }
        }

        return userReserves;

    }

    public bool RemoveByRoomId(Guid roomId)
    {
        var result = _reservationRepository.RemoveByRoomId(roomId);

        if (result is false)
            return false;

        return result;
    }

    public void UpdateReservationStatus(Guid id, ReservationStatus reservationStatus)
    {
        var reservation = GetById(id);

        reservation.UpdateReservationStatus(reservationStatus);

        var result = _reservationRepository.Update(reservation);

        if (result is false)
            throw new ReservationNotFoundException("reservation with this id not found!");
    }
}
