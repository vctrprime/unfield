using StadiumEngine.Domain.Entities.Settings;

namespace StadiumEngine.Domain.Services.Facades.Settings;

public interface IMainSettingsQueryFacade
{
    Task<MainSettings> GetByStadiumIdAsync( int stadiumId );
}