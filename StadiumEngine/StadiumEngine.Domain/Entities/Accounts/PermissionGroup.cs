using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace StadiumEngine.Domain.Entities.Accounts;

[Table("permission_group", Schema = "accounts")]
public class PermissionGroup : BaseEntity
{
    [Column("key")]
    public string Key { get; set; }
    
    [Column("sort")]
    [DefaultValue(1)]
    public int Sort { get; set; }
    
    public virtual ICollection<Permission> Permissions { get; set; }
}