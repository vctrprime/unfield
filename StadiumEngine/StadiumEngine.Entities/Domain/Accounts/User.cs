using System.ComponentModel.DataAnnotations.Schema;

namespace StadiumEngine.Entities.Domain.Accounts;

[Table("user", Schema = "accounts")]
public class User : BaseUserEntity
{
    [Column("last_name")]
    public string LastName { get; set; }
    
    [Column("phone_number")]
    public string PhoneNumber { get; set; }
    
    [Column("password")]
    public string Password { get; set; }
    
    [Column("legal_id")]
    public int LegalId { get; set; }
    
    [ForeignKey("LegalId")]
    public virtual Legal Legal { get; set; }
    
    [Column("role_id")]
    public int? RoleId { get; set; }
    
    [ForeignKey("RoleId")]
    public virtual Role Role { get; set; }
    
    [Column("is_superuser")]
    public bool IsSuperuser { get; set; }

}