using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Entities.Offers;

public abstract class BaseOffer : BaseUserEntity
{
    [Column("stadium_id")]
    public int StadiumId { get; set; }
    
    [ForeignKey("StadiumId")]
    public virtual Stadium Stadium { get; set; }
    
    public virtual ICollection<OffersSportKind> SportKinds { get; set; }
    public virtual ICollection<OffersImage> Images { get; set; }
}