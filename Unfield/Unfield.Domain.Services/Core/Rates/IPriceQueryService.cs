using Unfield.Domain.Entities.Rates;

namespace Unfield.Domain.Services.Core.Rates;

public interface IPriceQueryService
{
    Task<List<Price>> GetByStadiumIdAsync( int stadiumId );
}