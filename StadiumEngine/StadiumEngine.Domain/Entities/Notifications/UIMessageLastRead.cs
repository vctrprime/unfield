using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Entities.Notifications;

[Table( "ui_message_last_read", Schema = "notifications" )]
public class UIMessageLastRead : BaseEntity
{
    [NotMapped]
    public new string Name { get; set; }
    
    [NotMapped]
    public new string Description { get; set; }
    
    [Column( "stadium_id" )]
    public int StadiumId { get; set; }
    
    [Column( "ui_message_id" )]
    public int MessageId { get; set; }
    
    [Column( "user_id" )]
    public int UserId { get; set; }
    
    [ForeignKey( "StadiumId" )]
    public virtual Stadium Stadium { get; set; }
    
    [ForeignKey( "MessageId" )]
    public virtual UIMessage Message { get; set; }
    
    [ForeignKey( "UserId" )]
    public virtual User User { get; set; }
}