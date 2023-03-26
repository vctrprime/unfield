using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Repositories.Accounts;

public interface ILegalRepository
{
    Task<List<Legal>> GetByFilterAsync( string searchString );
    void Add( Legal legal );
}