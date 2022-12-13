using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Entities.Domain.Geo;

namespace StadiumEngine.Entities.Domain.Accounts;

public class Legal : BaseEntity
{
    public string Inn { get; set; }
    
    public string HeadName { get; set; }
    
    public int CityId { get; set; }
    
    [ForeignKey("CityId")]
    public virtual City City { get; set; }
}