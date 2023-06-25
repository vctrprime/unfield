using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Domain.Repositories.Settings;
using StadiumEngine.Domain.Services.Facades.Settings;

namespace StadiumEngine.Services.Facades.Settings;

internal class BreakQueryFacade : IBreakQueryFacade
{
    private readonly IBreakRepository _breakRepository;

    public BreakQueryFacade( IBreakRepository breakRepository )
    {
        _breakRepository = breakRepository;
    }

    public async Task<List<Break>> GetByStadiumIdAsync( int stadiumId ) => await _breakRepository.GetAllAsync( stadiumId );
}