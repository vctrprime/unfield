using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Entities.Geo;

[Table("city", Schema = "geo")]
public class City : BaseGeoEntity
{
    [Column("region_id")]
    public int RegionId { get; set; }
    
    [ForeignKey("RegionId")]
    public virtual Region Region { get; set; }
    
    public virtual ICollection<Legal> Legals { get; set; }
    public virtual ICollection<Stadium> Stadiums { get; set; }

}