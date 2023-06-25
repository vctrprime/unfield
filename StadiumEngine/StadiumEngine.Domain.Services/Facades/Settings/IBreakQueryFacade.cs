using StadiumEngine.Domain.Entities.Settings;

namespace StadiumEngine.Domain.Services.Facades.Settings;

public interface IBreakQueryFacade
{
    Task<List<Break>> GetByStadiumIdAsync( int stadiumId );
}