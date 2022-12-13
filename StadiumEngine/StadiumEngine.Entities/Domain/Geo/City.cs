using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Entities.Domain.Accounts;

namespace StadiumEngine.Entities.Domain.Geo;

public class City : BaseGeoEntity
{
    public int RegionId { get; set; }
    
    [ForeignKey("RegionId")]
    public virtual Region Region { get; set; }
    
    public virtual ICollection<Legal> Legals { get; set; }

}