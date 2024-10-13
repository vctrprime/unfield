using System.Collections.Generic;
using Unfield.Domain.Entities.Offers;

namespace Unfield.Domain.Entities.Rates;

public class PriceGroup : BaseRateEntity
{
    public virtual ICollection<Field> Fields { get; set; }
}