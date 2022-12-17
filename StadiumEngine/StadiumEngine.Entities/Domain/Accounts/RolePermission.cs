using System.ComponentModel.DataAnnotations.Schema;

namespace StadiumEngine.Entities.Domain.Accounts;

[Table("role_permission", Schema = "accounts")]
public class RolePermission : BaseUserEntity
{
    [Column("role_id")]
    public int RoleId { get; set; }
    
    [ForeignKey("RoleId")]
    public virtual Role Role { get; set; }
    
    [Column("permission_id")]
    public int PermissionId { get; set; }
    
    [ForeignKey("PermissionId")]
    public virtual Permission Permission { get; set; }
}