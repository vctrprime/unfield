using System.Collections.Generic;
using StadiumEngine.Common.Enums.Notifications;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Entities.Notifications;

public class UIMessage : BaseEntity
{
    public int StadiumId { get; set; }
    public UIMessageType MessageType { get; set; }
    public virtual Stadium Stadium { get; set; }
    
    public virtual ICollection<UIMessageText> Texts { get; set; }
    public virtual ICollection<UIMessageLastRead> UIMessageLastReads { get; set; }
}