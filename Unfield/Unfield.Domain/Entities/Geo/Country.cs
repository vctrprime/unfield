using System.Collections.Generic;

namespace Unfield.Domain.Entities.Geo;

public class Country : BaseGeoEntity
{
    public virtual ICollection<Region> Regions { get; set; }
}