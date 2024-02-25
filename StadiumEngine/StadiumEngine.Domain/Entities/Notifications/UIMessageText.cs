namespace StadiumEngine.Domain.Entities.Notifications;

public class UIMessageText : BaseEntity
{
    public int MessageId { get; set; }
    public int Index { get; set; }
    public string Text { get; set; }

    public virtual UIMessage Message { get; set; }
}