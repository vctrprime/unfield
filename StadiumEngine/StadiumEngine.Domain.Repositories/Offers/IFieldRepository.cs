#nullable enable
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Repositories.Offers;

public interface IFieldRepository
{
    Task<List<Field>> GetAllAsync( int stadiumId );
    Task<List<Field>> GetForCityAsync( int cityId, string? q );
    Task<Field?> GetAsync( int fieldId, int stadiumId );
    void Add( Field field );
    void Update( Field field );
    void Remove( Field field );
}