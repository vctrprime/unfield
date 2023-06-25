using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Domain.Repositories.Settings;
using StadiumEngine.Domain.Services.Facades.Settings;

namespace StadiumEngine.Services.Facades.Settings;

internal class MainSettingsQueryFacade : IMainSettingsQueryFacade
{
    private readonly IMainSettingsRepository _mainSettingsRepository;

    public MainSettingsQueryFacade( IMainSettingsRepository mainSettingsRepository )
    {
        _mainSettingsRepository = mainSettingsRepository;
    }
    
    public async Task<MainSettings> GetByStadiumIdAsync( int stadiumId ) =>
        await _mainSettingsRepository.GetAsync( stadiumId );
}