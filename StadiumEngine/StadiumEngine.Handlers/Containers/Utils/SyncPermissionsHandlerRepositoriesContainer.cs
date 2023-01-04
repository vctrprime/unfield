using StadiumEngine.Domain.Repositories.Accounts;

namespace StadiumEngine.Handlers.Containers.Utils;

internal class SyncPermissionsHandlerRepositoriesContainer
{
    public IPermissionRepository PermissionRepository { get; set; }
    public IPermissionGroupRepository PermissionGroupRepository { get; set; }
}