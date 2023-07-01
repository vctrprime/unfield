using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Domain.Services.Core.Rates;

public interface IPriceQueryService
{
    Task<List<Price>> GetByStadiumIdAsync( int stadiumId );
}