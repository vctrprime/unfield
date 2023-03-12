using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Domain.Repositories.Settings;
using StadiumEngine.Domain.Services.Facades.Settings;

namespace StadiumEngine.Services.Facades.Settings;

internal class StadiumMainSettingsCommandFacade : IStadiumMainSettingsCommandFacade
{
    private readonly IStadiumMainSettingsRepository _mainSettingsRepository;

    public StadiumMainSettingsCommandFacade( 
        IStadiumMainSettingsRepository mainSettingsRepository )
    {
        _mainSettingsRepository = mainSettingsRepository;
    }

    public void UpdateMainSettings( StadiumMainSettings mainSettings ) => _mainSettingsRepository.Update( mainSettings );
}