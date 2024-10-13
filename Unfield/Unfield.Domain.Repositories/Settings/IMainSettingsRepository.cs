using Unfield.Domain.Entities.Settings;

namespace Unfield.Domain.Repositories.Settings;

public interface IMainSettingsRepository
{
    Task<MainSettings> GetAsync( int stadiumId );
    Task<List<MainSettings>> GetAsync( List<int> stadiumsIds );
    void Update( MainSettings mainSettings );
}