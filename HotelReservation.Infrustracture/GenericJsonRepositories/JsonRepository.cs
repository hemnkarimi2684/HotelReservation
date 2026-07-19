using HotelReservation.Domain.Entities;
using HotelReservation.PathOfData;
using System.Text.Json;

namespace HotelReservation.Infrastructure.GenericJsonRepositories;

public class JsonRepository<T> : IJsonRepository<T> where T : BaseEntity
{
    private readonly string _filePath = DataPath<T>.EntityJson;

    public List<T> Read()
    {
        if(!File.Exists(_filePath))
            return new List<T>();

        var json = File.ReadAllText(_filePath);

        if(string.IsNullOrWhiteSpace(json))
            return new List<T>();

        var entities = JsonSerializer.Deserialize<List<T>>(json);

        return entities ?? new List<T>();
    }

    public void Save(List<T> values)
    {
        var option = new JsonSerializerOptions { WriteIndented = true };

        var json = JsonSerializer.Serialize(values, option);

        File.WriteAllText(_filePath, json);
    }
}
