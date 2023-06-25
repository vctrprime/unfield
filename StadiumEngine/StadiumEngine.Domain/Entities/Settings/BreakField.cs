using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Entities.Settings;

[Table( "break_field", Schema = "settings" )]
public class BreakField : BaseRefEntity
{
    [Column( "break_id" )]
    public int BreakId { get; set; }

    [ForeignKey( "BreakId" )]
    public virtual Break Break { get; set; }

    [Column( "field_id" )]
    public int FieldId { get; set; }

    [ForeignKey( "FieldId" )]
    public virtual Field Field { get; set; }
}