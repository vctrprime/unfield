using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Repositories.Accounts;

public interface IStadiumGroupRepository
{
    Task<List<StadiumGroup>> GetByFilterAsync( string searchString );
    void Add( StadiumGroup stadiumGroup );
}