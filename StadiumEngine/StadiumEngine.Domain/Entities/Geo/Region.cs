using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StadiumEngine.Domain.Entities.Geo;

[Table( "region", Schema = "geo" )]
public class Region : BaseGeoEntity
{
    [Column( "country_id" )]
    public int CountryId { get; set; }

    [ForeignKey( "CountryId" )]
    public virtual Country Country { get; set; }

    public virtual ICollection<City> Cities { get; set; }
}