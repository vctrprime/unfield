using StadiumEngine.Domain.Repositories.Accounts;

namespace StadiumEngine.Handlers.Containers.Utils;

internal class AddLegalHandlerRepositoriesContainer
{
    public ILegalRepository LegalRepository { get; set; }
    public IRoleRepository RoleRepository { get; set; }
    public IPermissionRepository PermissionRepository { get; set; }
    public IRolePermissionRepository RolePermissionRepository { get; set; }
    public IStadiumRepository StadiumRepository { get; set; } 
    public IRoleStadiumRepository RoleStadiumRepository { get; set; }
    public IUserRepository UserRepository { get; set; }
}