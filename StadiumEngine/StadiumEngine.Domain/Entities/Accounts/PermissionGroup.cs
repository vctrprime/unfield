using System.Collections.Generic;

namespace StadiumEngine.Domain.Entities.Accounts;

public class PermissionGroup : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Key { get; set; }
    public int Sort { get; set; }

    public virtual ICollection<Permission> Permissions { get; set; }
}