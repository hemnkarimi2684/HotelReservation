using HotelReservation.Domain.Enums;

namespace HotelReservation.Domain.Entities;

public class Room : BaseEntity
{
    public int RoomNumber { get; set; }

    public int Capacity { get; set; }

    public RoomType TypeOfRoom { get; set; }

    public decimal PricePerNight { get; set; }

    public bool IsAvailable { get; set; }

    public Room(int roomNumber, RoomType typeOfRoom, decimal pricePerNight, bool isAvailable)
    {
        RoomNumber = roomNumber;

        TypeOfRoom = typeOfRoom;

        PricePerNight = pricePerNight;

        IsAvailable = isAvailable;

        Validate();

        CalculateCapacity();
    }

    protected override void Validate()
    {
        if (RoomNumber < 1 || RoomNumber > 140)
            throw new ArgumentOutOfRangeException("invalid RoomNumber!");

        if (PricePerNight < 1)
            throw new ArgumentOutOfRangeException("invalid PricePerNight Number!");
    }

    public void SetCapacity(int capacity)
    {
        Capacity = capacity;
    }

    public void UpdateRoom(decimal pricePerNight, bool isAvailable)
    {
        PricePerNight = pricePerNight;

        IsAvailable = isAvailable;

        ModifiedAt = DateTime.UtcNow;
    }

    public void UpdateRoomStatus(bool isAvailable)
    {
        IsAvailable = isAvailable;
    }

    public void Delete()
    {
        DeletedAt = DateTime.UtcNow;

        IsAvailable = false;
    }

    private void CalculateCapacity()
    {
        if (TypeOfRoom.Equals(RoomType.SingleRoom))
            SetCapacity(1);

        else if (TypeOfRoom.Equals(RoomType.DoubleRoom))
            SetCapacity(2);

        else if (TypeOfRoom.Equals(RoomType.TripleRoom))
            SetCapacity(3);

        else if (TypeOfRoom.Equals(RoomType.FamilyRoom))
            SetCapacity(5);
    }
}
