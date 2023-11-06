using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Common.Enums.Notifications;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Entities.Notifications;

[Table( "ui_message", Schema = "notifications" )]
public class UIMessage : BaseEntity
{
    [NotMapped]
    public new string Name { get; set; }
    
    [NotMapped]
    public new string Description { get; set; }
    
    [Column( "stadium_id" )]
    public int StadiumId { get; set; }
    
    [Column( "message_type" )]
    public UIMessageType MessageType { get; set; }
    
    [ForeignKey( "StadiumId" )]
    public virtual Stadium Stadium { get; set; }
    
    public virtual ICollection<UIMessageText> Texts { get; set; }
    public virtual ICollection<UIMessageLastRead> UIMessageLastReads { get; set; }
}