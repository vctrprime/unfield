using StadiumEngine.Domain.Entities.Settings;

namespace StadiumEngine.Domain.Repositories.Settings;

public interface IStadiumMainSettingsRepository
{
    Task<StadiumMainSettings> GetAsync( int stadiumId );
    Task<List<StadiumMainSettings>> GetAsync( List<int> stadiumsIds );
    void Update( StadiumMainSettings mainSettings );
}