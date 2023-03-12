using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Entities.Settings;

[Table( "stadium_main_settings", Schema = "settings" )]
public class StadiumMainSettings : BaseUserEntity
{
    [Column( "stadium_id" )]
    public int StadiumId { get; set; }

    [ForeignKey( "StadiumId" )]
    public virtual Stadium Stadium { get; set; }
    
    [Column( "open_time" )]
    public string OpenTime { get; set; }
    
    [Column( "close_time" )]
    public string CloseTime { get; set; }
    
    [NotMapped]
    public new int? UserCreatedId { get; set; }
}