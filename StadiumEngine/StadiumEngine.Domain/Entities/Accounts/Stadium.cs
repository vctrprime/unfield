using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Domain.Entities.Geo;

namespace StadiumEngine.Domain.Entities.Accounts;

[Table("stadium", Schema = "accounts")]
public class Stadium : BaseEntity
{
    [Column("city_id")]
    public int CityId { get; set; }
    
    [ForeignKey("CityId")]
    public virtual City City { get; set; }
    
    [Column("legal_id")]
    public int LegalId { get; set; }
    
    [Column("is_deleted")]
    [DefaultValue(false)]
    public bool IsDeleted { get; set; }
    
    [ForeignKey("LegalId")]
    public virtual Legal Legal { get; set; }
    
    public string Address { get; set; }
    
    public virtual ICollection<RoleStadium> RoleStadiums { get; set; }
}