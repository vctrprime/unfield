using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Entities.Domain.Geo;

namespace StadiumEngine.Entities.Domain.Accounts;

[Table("legal", Schema = "accounts")]
public class Legal : BaseEntity
{
    [Column("inn")]
    public string Inn { get; set; }
    
    [Column("head_name")]
    public string HeadName { get; set; }
    
    [Column("city_id")]
    public int CityId { get; set; }
    
    [ForeignKey("CityId")]
    public virtual City City { get; set; }
    
    
    
    public virtual ICollection<Role> Roles { get; set; }
    public virtual ICollection<User> Users { get; set; }
}