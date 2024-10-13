using Unfield.Domain.Entities.Settings;
using Unfield.Domain.Repositories.Settings;
using Unfield.Domain.Services.Core.Settings;

namespace Unfield.Services.Core.Settings;

internal class BreakQueryService : IBreakQueryService
{
    private readonly IBreakRepository _breakRepository;

    public BreakQueryService( IBreakRepository breakRepository )
    {
        _breakRepository = breakRepository;
    }

    public async Task<List<Break>> GetByStadiumIdAsync( int stadiumId ) =>
        await _breakRepository.GetAllAsync( stadiumId );

    public async Task<Break?> GetByBreakIdAsync( int breakId, int stadiumId ) =>
        await _breakRepository.GetAsync( breakId, stadiumId );
}