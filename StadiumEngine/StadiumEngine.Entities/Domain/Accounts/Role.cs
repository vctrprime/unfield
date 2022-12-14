using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StadiumEngine.Entities.Domain.Accounts;

[Table("role", Schema = "accounts")]
public class Role : BaseUserEntity
{
    [Column("legal_id")]
    public int LegalId { get; set; }
    
    [ForeignKey("LegalId")]
    public virtual Legal Legal { get; set; }
    
    public virtual ICollection<User> Users { get; set; }
}