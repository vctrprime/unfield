using System.Collections.Generic;

namespace StadiumEngine.Entities.Domain.Geo;

public class Country : BaseGeoEntity
{
    public virtual ICollection<Region> Regions { get; set; }
}