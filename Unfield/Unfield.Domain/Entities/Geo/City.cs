using System.Collections.Generic;
using Unfield.Domain.Entities.Accounts;

namespace Unfield.Domain.Entities.Geo;

public class City : BaseGeoEntity
{
    public int RegionId { get; set; }
    public virtual Region Region { get; set; }
    public virtual ICollection<Stadium> Stadiums { get; set; }
}