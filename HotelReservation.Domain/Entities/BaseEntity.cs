
namespace HotelReservation.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public BaseEntity()
    {
        Id = Guid.NewGuid();

        CreatedAt = DateTime.UtcNow;
    }

    public void SetDeletedAt(DateTime? deletedAt = null)
    {
       DeletedAt = deletedAt;
    }

    protected abstract void Validate();
}
