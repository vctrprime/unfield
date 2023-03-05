using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities.Geo;

namespace StadiumEngine.Domain.Entities.Accounts;

[Table( "legal", Schema = "accounts" )]
[Index( nameof( Inn ), IsUnique = true )]
public class Legal : BaseEntity
{
    [Column( "inn" )] public string Inn { get; set; }

    [Column( "head_name" )] public string HeadName { get; set; }

    [Column( "city_id" )] public int CityId { get; set; }

    [ForeignKey( "CityId" )] public virtual City City { get; set; }


    public virtual ICollection<Role> Roles { get; set; }
    public virtual ICollection<User> Users { get; set; }
    public virtual ICollection<Stadium> Stadiums { get; set; }
}