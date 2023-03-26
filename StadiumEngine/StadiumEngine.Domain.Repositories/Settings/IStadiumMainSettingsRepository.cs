using StadiumEngine.Domain.Entities.Settings;

namespace StadiumEngine.Domain.Repositories.Settings;

public interface IStadiumMainSettingsRepository
{
    Task<StadiumMainSettings> GetAsync( int stadiumId );
    void Update( StadiumMainSettings mainSettings );
}