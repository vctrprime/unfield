using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StadiumEngine.Domain.Entities.Notifications;

[Table( "ui_message_text", Schema = "notifications" )]
public class UIMessageText
{
    [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
    [Key]
    [Column( "id", Order = 0 )]
    public int Id { get; set; }
    
    [Column( "ui_message_id" )]
    public int MessageId { get; set; }
    
    [Column( "index" )]
    public int Index { get; set; }
    
    [Column( "text" )]
    public string Text { get; set; }
    
    [ForeignKey( "MessageId" )]
    public virtual UIMessage Message { get; set; }
}