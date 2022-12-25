using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StadiumEngine.Entities.Domain.Accounts;

[Table("permission_group", Schema = "accounts")]
public class PermissionGroup : BaseEntity
{
    [Column("key")]
    public string Key { get; set; }
    
    public virtual ICollection<Permission> Permissions { get; set; }
}