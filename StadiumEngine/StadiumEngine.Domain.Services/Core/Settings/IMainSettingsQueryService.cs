using StadiumEngine.Domain.Entities.Settings;

namespace StadiumEngine.Domain.Services.Core.Settings;

public interface IMainSettingsQueryService
{
    Task<MainSettings> GetByStadiumIdAsync( int stadiumId );
}