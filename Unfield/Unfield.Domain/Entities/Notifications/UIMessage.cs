using System.Collections.Generic;
using Unfield.Common.Enums.Notifications;
using Unfield.Domain.Entities.Accounts;

namespace Unfield.Domain.Entities.Notifications;

public class UIMessage : BaseEntity
{
    public int StadiumId { get; set; }
    public UIMessageType MessageType { get; set; }
    public virtual Stadium Stadium { get; set; }
    
    public virtual ICollection<UIMessageText> Texts { get; set; }
    public virtual ICollection<UIMessageLastRead> UIMessageLastReads { get; set; }
}