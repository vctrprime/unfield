using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Entities.Accounts;

[Table("role_stadium", Schema = "accounts")]
public class RoleStadium : BaseUserEntity
{
    [Column("role_id")]
    public int RoleId { get; set; }
    
    [ForeignKey("RoleId")]
    public virtual Role Role { get; set; }
    
    [Column("stadium_id")]
    public int StadiumId { get; set; }
    
    [ForeignKey("StadiumId")]
    public virtual Stadium Stadium { get; set; }
}