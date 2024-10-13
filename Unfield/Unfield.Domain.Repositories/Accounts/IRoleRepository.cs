#nullable enable
using Unfield.Domain.Entities.Accounts;

namespace Unfield.Domain.Repositories.Accounts;

public interface IRoleRepository
{
    Task<List<Role>> GetAllAsync( int stadiumGroupId );
    Task<Role?> GetAsync( int roleId );
    void Add( Role role );
    void Update( Role role );
    void Remove( Role role );
}