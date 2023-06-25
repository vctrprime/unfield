using StadiumEngine.Domain.Entities.Settings;

namespace StadiumEngine.Domain.Services.Facades.Settings;

public interface IMainSettingsCommandFacade
{
    void UpdateMainSettings( MainSettings mainSettings );
}