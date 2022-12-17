using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Entities.Domain.Offers;

namespace StadiumEngine.Entities.Domain.Accounts;

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