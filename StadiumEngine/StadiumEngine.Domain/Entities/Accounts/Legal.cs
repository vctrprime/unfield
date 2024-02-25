using System.Collections.Generic;
using StadiumEngine.Domain.Entities.Geo;

namespace StadiumEngine.Domain.Entities.Accounts;

public class Legal : BaseEntity
{
    public string Inn { get; set; }
    public string HeadName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int CityId { get; set; }
    public virtual City City { get; set; }
    
    public virtual ICollection<Role> Roles { get; set; }
    public virtual ICollection<User> Users { get; set; }
    public virtual ICollection<Stadium> Stadiums { get; set; }
}