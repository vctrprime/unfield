using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Domain.Entities.Geo;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Entities.Settings;

namespace StadiumEngine.Domain.Entities.Accounts;

[Table( "stadium", Schema = "accounts" )]
public class Stadium : BaseEntity
{
    [Column( "city_id" )]
    public int CityId { get; set; }

    [ForeignKey( "CityId" )]
    public virtual City City { get; set; }

    [Column( "legal_id" )]
    public int LegalId { get; set; }

    [Column( "is_deleted" )]
    [DefaultValue( false )]
    public bool IsDeleted { get; set; }

    [ForeignKey( "LegalId" )]
    public virtual Legal Legal { get; set; }

    [Column( "address" )]
    public string Address { get; set; }
    
    [Column( "token" )]
    public string Token { get; set; }

    public virtual ICollection<RoleStadium> RoleStadiums { get; set; }
    public virtual ICollection<LockerRoom> LockerRooms { get; set; }
    public virtual ICollection<Field> Fields { get; set; }
    public virtual ICollection<Tariff> Tariffs { get; set; }
    public virtual ICollection<PriceGroup> PriceGroups { get; set; }
    public virtual ICollection<Break> Breaks { get; set; }
    
    public virtual StadiumMainSettings MainSettings { get; set; }
}