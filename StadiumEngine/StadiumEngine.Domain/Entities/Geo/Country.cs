using System.Collections.Generic;

namespace StadiumEngine.Domain.Entities.Geo;

public class Country : BaseGeoEntity
{
    public virtual ICollection<Region> Regions { get; set; }
}