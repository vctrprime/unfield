using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StadiumEngine.Entities.Domain.Accounts;

[Table("permission", Schema = "accounts")]
public class Permission : BaseEntity
{
    [Column("permission_group_id")]
    public int PermissionGroupId { get; set; }
    
    [ForeignKey("PermissionGroupId")]
    public virtual PermissionGroup PermissionGroup { get; set; }
    
    public virtual ICollection<RolePermission> RolePermissions { get; set; }
}