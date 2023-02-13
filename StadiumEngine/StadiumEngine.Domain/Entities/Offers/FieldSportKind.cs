using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Common.Enums.Offers;

namespace StadiumEngine.Domain.Entities.Offers;

[Table("field_sport", Schema = "offers")]
public class FieldSportKind : BaseRefEntity
{
    [Column("field_id")]
    public int FieldId { get; set; }
    
    [ForeignKey("FieldId")]
    public virtual Field Field { get; set; }
    
    [Column("sport_kind")]
    public SportKind SportKind { get; set; }
    
}