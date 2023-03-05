#nullable enable
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Repositories.Accounts;

public interface IRoleRepository
{
    Task<List<Role>> GetAll( int legalId );
    Task<Role?> Get( int roleId );
    void Add( Role role );
    void Update( Role role );
    void Remove( Role role );
}