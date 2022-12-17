using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Entities.Domain.Accounts;
using StadiumEngine.Entities.Domain.Geo;

namespace StadiumEngine.Entities.Domain.Offers;

[Table("stadium", Schema = "offers")]
public class Stadium : BaseEntity
{
    [Column("city_id")]
    public int CityId { get; set; }
    
    [ForeignKey("CityId")]
    public virtual City City { get; set; }
    
    [Column("legal_id")]
    public int LegalId { get; set; }
    
    [ForeignKey("LegalId")]
    public virtual Legal Legal { get; set; }
    
    public string Address { get; set; }
    
    public virtual ICollection<RoleStadium> RoleStadiums { get; set; }
}