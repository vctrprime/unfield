using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Domain.Repositories.Settings;
using StadiumEngine.Domain.Services.Facades.Settings;

namespace StadiumEngine.Services.Facades.Settings;

internal class StadiumMainSettingsQueryFacade : IStadiumMainSettingsQueryFacade
{
    private readonly IStadiumMainSettingsRepository _mainSettingsRepository;

    public StadiumMainSettingsQueryFacade( IStadiumMainSettingsRepository mainSettingsRepository )
    {
        _mainSettingsRepository = mainSettingsRepository;
    }
    
    public async Task<StadiumMainSettings> GetByStadiumIdAsync( int stadiumId ) =>
        await _mainSettingsRepository.GetAsync( stadiumId );
}