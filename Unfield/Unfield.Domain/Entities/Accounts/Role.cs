using System.Collections.Generic;

namespace Unfield.Domain.Entities.Accounts;

public class Role : BaseUserEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsDeleted { get; set; }
    public int StadiumGroupId { get; set; }

    public virtual StadiumGroup StadiumGroup { get; set; }

    public virtual ICollection<User> Users { get; set; }
    public virtual ICollection<RolePermission> RolePermissions { get; set; }
}