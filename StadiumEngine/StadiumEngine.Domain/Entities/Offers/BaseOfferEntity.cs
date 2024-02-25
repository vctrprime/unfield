using System.Collections.Generic;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Entities.Offers;

public abstract class BaseOfferEntity : BaseUserEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int StadiumId { get; set; }
    public virtual Stadium Stadium { get; set; }
    
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }

    public virtual ICollection<OffersSportKind> SportKinds { get; set; }
    public virtual ICollection<OffersImage> Images { get; set; }
}