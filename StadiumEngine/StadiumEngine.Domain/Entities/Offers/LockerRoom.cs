using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Common.Enums.Offers;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Entities.Offers;

[Table("locker_room", Schema = "offers")]
public class LockerRoom : BaseUserEntity
{
    [Column("gender")]
    public LockerRoomGender Gender { get; set; }
    
    [Column("is_active")]
    public bool IsActive { get; set; }
    
    [Column("is_deleted")]
    public bool IsDeleted { get; set; }
    
    [Column("stadium_id")]
    public int StadiumId { get; set; }
    
    [ForeignKey("StadiumId")]
    public virtual Stadium Stadium { get; set; }
}