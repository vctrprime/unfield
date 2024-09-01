using System.Collections.Generic;
using StadiumEngine.Domain.Entities.Customers;

namespace StadiumEngine.Domain.Entities.Accounts;

public class StadiumGroup : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    
    public virtual ICollection<Role> Roles { get; set; }
    public virtual ICollection<User> Users { get; set; }
    public virtual ICollection<Stadium> Stadiums { get; set; }
    public virtual ICollection<Customer> Customers { get; set; }
}