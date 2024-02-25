using System.Collections.Generic;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Entities.Geo;

public class City : BaseGeoEntity
{
    public int RegionId { get; set; }
    public virtual Region Region { get; set; }

    public virtual ICollection<Legal> Legals { get; set; }
    public virtual ICollection<Stadium> Stadiums { get; set; }
}