using Unfield.Domain.Entities.Settings;
using Unfield.Domain.Repositories.Settings;
using Unfield.Domain.Services.Core.Settings;

namespace Unfield.Services.Core.Settings;

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