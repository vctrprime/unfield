using StadiumEngine.Domain.Entities.Settings;

namespace StadiumEngine.Domain.Services.Core.Settings;

public interface IMainSettingsCommandService
{
    void UpdateMainSettings( MainSettings mainSettings );
}