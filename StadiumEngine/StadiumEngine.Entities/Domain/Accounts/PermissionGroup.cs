using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StadiumEngine.Entities.Domain.Accounts;

[Table("permission_group", Schema = "accounts")]
public class PermissionGroup : BaseEntity
{
    public virtual ICollection<Permission> Permissions { get; set; }
}