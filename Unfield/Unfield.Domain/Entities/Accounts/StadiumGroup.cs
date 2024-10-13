using System.Collections.Generic;
using Unfield.Domain.Entities.Customers;

namespace Unfield.Domain.Entities.Accounts;

public class StadiumGroup : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    
    public virtual ICollection<Role> Roles { get; set; }
    public virtual ICollection<User> Users { get; set; }
    public virtual ICollection<Stadium> Stadiums { get; set; }
    public virtual ICollection<Customer> Customers { get; set; }
}