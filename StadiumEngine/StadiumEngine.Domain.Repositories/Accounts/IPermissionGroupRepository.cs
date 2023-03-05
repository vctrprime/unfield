using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Repositories.Accounts;

public interface IPermissionGroupRepository
{
    void Add( PermissionGroup permissionGroup );
    void Update( PermissionGroup permissionGroup );
}