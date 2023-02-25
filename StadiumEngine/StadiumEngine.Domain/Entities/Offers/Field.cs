#nullable enable
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Common.Enums.Offers;

namespace StadiumEngine.Domain.Entities.Offers;

[Table("field", Schema = "offers")]
public class Field : BaseOffer
{
    [Column("parent_field_id")]
    public int? ParentFieldId { get; set; }
    
    [ForeignKey("ParentFieldId")]
    public virtual Field? ParentField { get; set; }
    
    [Column("covering_type")]
    public CoveringType CoveringType { get; set; }
    
    [Column("width")]
    public decimal Width { get; set; }
    
    [Column("length")]
    public decimal Length { get; set; }
    
    [Column("is_deleted")]
    public bool IsDeleted { get; set; }
    
    [Column("is_active")]
    public bool IsActive { get; set; }
    
    public virtual ICollection<Field> ChildFields { get; set; }
}