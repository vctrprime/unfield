using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Domain.Repositories.Settings;
using StadiumEngine.Domain.Services.Application.Settings;

namespace StadiumEngine.Services.Application.Settings;

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