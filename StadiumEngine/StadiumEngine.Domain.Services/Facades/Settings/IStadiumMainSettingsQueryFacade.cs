using StadiumEngine.Domain.Entities.Settings;

namespace StadiumEngine.Domain.Services.Facades.Settings;

public interface IStadiumMainSettingsQueryFacade
{
    Task<StadiumMainSettings> GetByStadiumId( int stadiumId );
}