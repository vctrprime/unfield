using Unfield.Domain.Entities.Settings;

namespace Unfield.Domain.Services.Core.Settings;

public interface IBreakQueryService
{
    Task<List<Break>> GetByStadiumIdAsync( int stadiumId );
    Task<Break?> GetByBreakIdAsync( int breakId, int stadiumId );
}