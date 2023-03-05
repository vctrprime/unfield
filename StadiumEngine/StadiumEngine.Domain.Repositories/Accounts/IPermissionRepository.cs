using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Repositories.Accounts;

public interface IPermissionRepository
{
    Task<List<Permission>> GetAll();
    Task<List<Permission>> GetForRole( int roleId );
    void Add( Permission permission );
    void Update( Permission permission );
}