using Unfield.Domain.Entities.Settings;

namespace Unfield.Domain.Services.Core.Settings;

public interface IMainSettingsQueryService
{
    Task<MainSettings> GetByStadiumIdAsync( int stadiumId );
}