using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Domain.Repositories.Settings;
using StadiumEngine.Domain.Services.Application.Settings;

namespace StadiumEngine.Services.Application.Settings;

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