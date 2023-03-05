using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Entities.Offers;

public abstract class BaseOfferEntity : BaseUserEntity
{
    [Column( "stadium_id" )]
    public int StadiumId { get; set; }

    [ForeignKey( "StadiumId" )]
    public virtual Stadium Stadium { get; set; }

    [Column( "is_active" )]
    public bool IsActive { get; set; }

    [Column( "is_deleted" )]
    public bool IsDeleted { get; set; }

    public virtual ICollection<OffersSportKind> SportKinds { get; set; }
    public virtual ICollection<OffersImage> Images { get; set; }
}