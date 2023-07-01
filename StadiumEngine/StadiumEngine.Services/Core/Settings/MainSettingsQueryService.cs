using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Domain.Repositories.Settings;
using StadiumEngine.Domain.Services.Core.Settings;

namespace StadiumEngine.Services.Core.Settings;

internal class MainSettingsQueryService : IMainSettingsQueryService
{
    private readonly IMainSettingsRepository _mainSettingsRepository;

    public MainSettingsQueryService( IMainSettingsRepository mainSettingsRepository )
    {
        _mainSettingsRepository = mainSettingsRepository;
    }
    
    public async Task<MainSettings> GetByStadiumIdAsync( int stadiumId ) =>
        await _mainSettingsRepository.GetAsync( stadiumId );
}