using Unfield.Domain.Entities.Accounts;

namespace Unfield.Domain.Repositories.Accounts;

public interface IPermissionGroupRepository
{
    void Add( PermissionGroup permissionGroup );
    void Update( PermissionGroup permissionGroup );
}