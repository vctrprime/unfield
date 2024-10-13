namespace Unfield.Domain.Entities.Accounts;

public class RolePermission : BaseUserEntity
{
    public int RoleId { get; set; }
    public virtual Role Role { get; set; }
    
    public int PermissionId { get; set; }
    public virtual Permission Permission { get; set; }
}