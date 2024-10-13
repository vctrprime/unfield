using Unfield.Domain.Entities.Settings;
using Unfield.Domain.Repositories.Settings;
using Unfield.Domain.Services.Core.Settings;

namespace Unfield.Services.Core.Settings;

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