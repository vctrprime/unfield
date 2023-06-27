using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Domain.Services.Application.Rates;

public interface IPriceQueryService
{
    Task<List<Price>> GetByStadiumIdAsync( int stadiumId );
}