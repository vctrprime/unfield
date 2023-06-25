using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Domain.Repositories.Settings;
using StadiumEngine.Domain.Services.Facades.Settings;

namespace StadiumEngine.Services.Facades.Settings;

internal class MainSettingsCommandFacade : IMainSettingsCommandFacade
{
    private readonly IMainSettingsRepository _mainSettingsRepository;

    public MainSettingsCommandFacade( 
        IMainSettingsRepository mainSettingsRepository )
    {
        _mainSettingsRepository = mainSettingsRepository;
    }

    public void UpdateMainSettings( MainSettings mainSettings ) => _mainSettingsRepository.Update( mainSettings );
}