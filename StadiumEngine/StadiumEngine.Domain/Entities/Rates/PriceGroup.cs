using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Entities.Rates;

[Table("price_group", Schema = "rates")]
public class PriceGroup : BaseRateEntity
{
    public virtual ICollection<Field> Fields { get; set; }
}