using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Domain.Repositories.Settings;
using StadiumEngine.Domain.Services.Core.Settings;

namespace StadiumEngine.Services.Core.Settings;

internal class MainSettingsCommandService : IMainSettingsCommandService
{
    private readonly IMainSettingsRepository _mainSettingsRepository;

    public MainSettingsCommandService( 
        IMainSettingsRepository mainSettingsRepository )
    {
        _mainSettingsRepository = mainSettingsRepository;
    }

    public void UpdateMainSettings( MainSettings mainSettings ) => _mainSettingsRepository.Update( mainSettings );
}