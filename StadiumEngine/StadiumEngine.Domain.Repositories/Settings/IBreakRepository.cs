using StadiumEngine.Domain.Entities.Settings;

namespace StadiumEngine.Domain.Repositories.Settings;

public interface IBreakRepository
{
    Task<List<Break>> GetAllAsync( int stadiumId );
}