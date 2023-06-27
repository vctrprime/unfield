using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Entities.Settings;

[Table( "main_settings", Schema = "settings" )]
public class MainSettings : BaseUserEntity
{
    [Column( "stadium_id" )]
    public int StadiumId { get; set; }

    [ForeignKey( "StadiumId" )]
    public virtual Stadium Stadium { get; set; }
    
    [Column( "open_time" )]
    public int OpenTime { get; set; }
    
    [Column( "close_time" )]
    public int CloseTime { get; set; }
    
    [NotMapped]
    public new int? UserCreatedId { get; set; }
    
    [NotMapped]
    public new virtual User UserCreated { get; set; }
}