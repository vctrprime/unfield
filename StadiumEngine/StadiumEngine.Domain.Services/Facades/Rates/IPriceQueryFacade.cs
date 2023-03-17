using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Domain.Services.Facades.Rates;

public interface IPriceQueryFacade
{
    Task<List<Price>> GetByStadiumId( int stadiumId );
}