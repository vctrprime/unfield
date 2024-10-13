using Unfield.Domain.Entities.Accounts;

namespace Unfield.Domain.Entities.Rates;

public abstract class BaseRateEntity : BaseUserEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int StadiumId { get; set; }

    public virtual Stadium Stadium { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
}