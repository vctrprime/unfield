#nullable enable
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Services.Facades.Offers;

public interface IFieldQueryFacade
{
    Task<List<Field>> GetByStadiumIdAsync( int stadiumId );
    Task<Field?> GetByFieldIdAsync( int fieldId, int stadiumId );
}