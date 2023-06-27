#nullable enable
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Services.Application.Offers;

public interface IFieldQueryService
{
    Task<List<Field>> GetByStadiumIdAsync( int stadiumId );
    Task<Field?> GetByFieldIdAsync( int fieldId, int stadiumId );
}