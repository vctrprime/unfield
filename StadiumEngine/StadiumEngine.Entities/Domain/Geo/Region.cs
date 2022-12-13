using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StadiumEngine.Entities.Domain.Geo;

public class Region : BaseGeoEntity
{
    public int CountryId { get; set; }
    
    [ForeignKey("CountryId")]
    public virtual Country Country { get; set; }
    
    public virtual ICollection<City> Cities { get; set; }
}