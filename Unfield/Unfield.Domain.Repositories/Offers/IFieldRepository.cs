#nullable enable
using Unfield.Domain.Entities.Offers;

namespace Unfield.Domain.Repositories.Offers;

public interface IFieldRepository
{
    Task<List<Field>> GetAllAsync( int stadiumId );
    Task<List<Field>> GetForCityAsync( int cityId, string? q );
    Task<Field?> GetAsync( int fieldId, int stadiumId );
    Task<Field?> GetAsync( int fieldId );
    void Add( Field field );
    void Update( Field field );
    void Remove( Field field );
}