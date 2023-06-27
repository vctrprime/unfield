using StadiumEngine.Domain.Entities.Settings;

namespace StadiumEngine.Domain.Services.Application.Settings;

public interface IMainSettingsCommandService
{
    void UpdateMainSettings( MainSettings mainSettings );
}