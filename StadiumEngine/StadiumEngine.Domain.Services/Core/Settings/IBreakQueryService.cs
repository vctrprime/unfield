using StadiumEngine.Domain.Entities.Settings;

namespace StadiumEngine.Domain.Services.Core.Settings;

public interface IBreakQueryService
{
    Task<List<Break>> GetByStadiumIdAsync( int stadiumId );
    Task<Break?> GetByBreakIdAsync( int breakId, int stadiumId );
}