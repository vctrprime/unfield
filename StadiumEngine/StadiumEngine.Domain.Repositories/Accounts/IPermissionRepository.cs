using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Repositories.Accounts;

public interface IPermissionRepository
{
    Task<List<Permission>> GetAllAsync();
    Task<List<Permission>> GetForRoleAsync( int roleId );
    void Add( Permission permission );
    void Update( Permission permission );
}