using StadiumEngine.Domain.Entities.Settings;

namespace StadiumEngine.Domain.Repositories.Settings;

public interface IStadiumMainSettingsRepository
{
    Task<StadiumMainSettings> Get( int stadiumId );
    void Update( StadiumMainSettings mainSettings );
}