using Unfield.Domain.Entities.Accounts;

namespace Unfield.Domain.Repositories.Accounts;

public interface IStadiumGroupRepository
{
    Task<List<StadiumGroup>> GetByFilterAsync( string searchString );
    void Add( StadiumGroup stadiumGroup );
}