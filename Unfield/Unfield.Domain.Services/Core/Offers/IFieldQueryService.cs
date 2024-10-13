#nullable enable
using Unfield.Domain.Entities.Offers;

namespace Unfield.Domain.Services.Core.Offers;

public interface IFieldQueryService
{
    Task<List<Field>> GetByStadiumIdAsync( int stadiumId );
    Task<Field?> GetByFieldIdAsync( int fieldId, int stadiumId );
}