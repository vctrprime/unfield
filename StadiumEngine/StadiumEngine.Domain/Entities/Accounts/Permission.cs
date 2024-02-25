using System.Collections.Generic;

namespace StadiumEngine.Domain.Entities.Accounts;

public class Permission : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string DisplayName { get; set; }
    public int PermissionGroupId { get; set; }
    public int Sort { get; set; }
    public virtual PermissionGroup PermissionGroup { get; set; }

    public virtual ICollection<RolePermission> RolePermissions { get; set; }
}