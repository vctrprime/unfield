using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StadiumEngine.Domain.Entities.Accounts;

[Table("permission", Schema = "accounts")]
public class Permission : BaseEntity
{
    [Column("action")]
    public string Action { get; set; }
    
    [Column("display_name")]
    public string DisplayName { get; set; }
    
    [Column("permission_group_id")]
    public int PermissionGroupId { get; set; }
    
    [ForeignKey("PermissionGroupId")]
    public virtual PermissionGroup PermissionGroup { get; set; }
    
    public virtual ICollection<RolePermission> RolePermissions { get; set; }
}