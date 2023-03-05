using System.ComponentModel.DataAnnotations.Schema;

namespace StadiumEngine.Domain.Entities.Accounts;

[Table( "role_permission", Schema = "accounts" )]
public class RolePermission : BaseRefEntity
{
    [Column( "role_id" )]
    public int RoleId { get; set; }

    [ForeignKey( "RoleId" )]
    public virtual Role Role { get; set; }

    [Column( "permission_id" )]
    public int PermissionId { get; set; }

    [ForeignKey( "PermissionId" )]
    public virtual Permission Permission { get; set; }
}