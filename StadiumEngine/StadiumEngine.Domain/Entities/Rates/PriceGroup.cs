using System.Collections.Generic;
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Entities.Rates;

public class PriceGroup : BaseRateEntity
{
    public virtual ICollection<Field> Fields { get; set; }
}