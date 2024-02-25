using System.Collections.Generic;

namespace StadiumEngine.Domain.Entities.Accounts;

public class Role : BaseUserEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsDeleted { get; set; }
    public int LegalId { get; set; }

    public virtual Legal Legal { get; set; }

    public virtual ICollection<User> Users { get; set; }
    public virtual ICollection<RolePermission> RolePermissions { get; set; }
}