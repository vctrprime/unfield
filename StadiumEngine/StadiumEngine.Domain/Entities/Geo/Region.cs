using System.Collections.Generic;

namespace StadiumEngine.Domain.Entities.Geo;

public class Region : BaseGeoEntity
{
    public int CountryId { get; set; }
    public virtual Country Country { get; set; }

    public virtual ICollection<City> Cities { get; set; }
}