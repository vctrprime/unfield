using Unfield.Domain.Entities.Settings;

namespace Unfield.Domain.Services.Core.Settings;

public interface IMainSettingsCommandService
{
    void UpdateMainSettings( MainSettings mainSettings );
}