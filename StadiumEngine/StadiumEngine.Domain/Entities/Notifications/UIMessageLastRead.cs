using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Entities.Notifications;

public class UIMessageLastRead : BaseEntity
{
    public int StadiumId { get; set; }
    public int MessageId { get; set; }
    public int UserId { get; set; }
    
    public virtual Stadium Stadium { get; set; }
    public virtual UIMessage Message { get; set; }
    public virtual User User { get; set; }
}