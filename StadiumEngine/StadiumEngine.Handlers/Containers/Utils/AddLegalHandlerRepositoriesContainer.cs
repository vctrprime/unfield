using StadiumEngine.Domain.Repositories.Accounts;

namespace StadiumEngine.Handlers.Containers.Utils;

internal class AddLegalHandlerRepositoriesContainer
{
    public ILegalRepository LegalRepository { get; set; }
    public IPermissionRepository PermissionRepository { get; set; }
}