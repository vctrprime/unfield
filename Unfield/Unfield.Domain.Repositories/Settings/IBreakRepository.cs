using Unfield.Domain.Entities.Settings;

namespace Unfield.Domain.Repositories.Settings;

public interface IBreakRepository
{
    Task<List<Break>> GetAllAsync( int stadiumId );
    Task<Break?> GetAsync( int breakId, int stadiumId );
    void Add( Break @break );
    void Update( Break @break );
    void Remove( Break @break );
}