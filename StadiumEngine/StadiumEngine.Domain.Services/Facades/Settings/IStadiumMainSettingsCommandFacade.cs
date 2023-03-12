using StadiumEngine.Domain.Entities.Settings;

namespace StadiumEngine.Domain.Services.Facades.Settings;

public interface IStadiumMainSettingsCommandFacade
{
    void UpdateMainSettings( StadiumMainSettings mainSettings );
}