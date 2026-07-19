using HotelReservation.Domain.Entities;

namespace HotelReservation.PathOfData;

public static class DataPath<T> where T : BaseEntity
{
    private static readonly string _baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

    public static readonly string DataFolder = Path.Combine(_baseDirectory, "Data");

    public static readonly string EntityJson = Path.Combine(DataFolder, $"{typeof(T).Name}.json");

    static DataPath()
    {
        Directory.CreateDirectory(DataFolder);
    }
}

