using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StadiumEngine.Domain.Entities.Geo;

[Table( "country", Schema = "geo" )]
public class Country : BaseGeoEntity
{
    public virtual ICollection<Region> Regions { get; set; }
}