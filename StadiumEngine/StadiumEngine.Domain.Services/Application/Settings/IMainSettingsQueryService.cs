using StadiumEngine.Domain.Entities.Settings;

namespace StadiumEngine.Domain.Services.Application.Settings;

public interface IMainSettingsQueryService
{
    Task<MainSettings> GetByStadiumIdAsync( int stadiumId );
}