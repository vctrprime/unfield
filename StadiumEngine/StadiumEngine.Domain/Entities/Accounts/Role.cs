using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StadiumEngine.Domain.Entities.Accounts;

[Table("role", Schema = "accounts")]
public class Role : BaseUserEntity
{
    [Column("legal_id")]
    public int LegalId { get; set; }
    
    [ForeignKey("LegalId")]
    public virtual Legal Legal { get; set; }
    
    public virtual ICollection<User> Users { get; set; }
    public virtual ICollection<RolePermission> RolePermissions { get; set; }
    public virtual ICollection<RoleStadium> RoleStadiums { get; set; }
}