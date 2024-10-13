#nullable enable
using Unfield.Domain.Entities.Accounts;

namespace Unfield.Domain.Entities;

public abstract class BaseUserEntity : BaseEntity
{
    public int? UserCreatedId { get; set; }
    public int? UserModifiedId { get; set; }

    public virtual User? UserCreated { get; set; }
    public virtual User? UserModified { get; set; }
}